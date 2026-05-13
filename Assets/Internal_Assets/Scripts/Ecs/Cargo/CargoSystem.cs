using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class CargoSystem : ISystem
    {
        private Filter _filter;
        private Stash<CargoViewComponent> _cargoViewStash;
        private Request<CargoRequest> _cargoRequest;

        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            _cargoRequest = World.GetRequest<CargoRequest>();
            _filter = World.Filter.With<CargoViewComponent>().Build();
            _cargoViewStash = World.GetStash<CargoViewComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var request in _cargoRequest.Consume())
            {
                ApplyCargo(request.Cargo);
            }
        }
        
        private void ApplyCargo(int cargo)
        {
            PlayerStats.Instance.AddCargo(cargo);

            foreach (var entity in _filter)
            {
                ref var c = ref _cargoViewStash.Get(entity);
                c.ViewLink.UpdateValues();
            }
        }
    }
}
