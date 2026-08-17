using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class CargoUpgradeSystem : ISystem
    {
        private Filter _filter;
        private Stash<CargoViewComponent> _cargoViewStash;
        private Request<CargoUpgradeRequest> _cargoUpgradeRequest;
        
        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            _cargoUpgradeRequest = World.GetRequest<CargoUpgradeRequest>();
            _filter = World.Filter.With<CargoViewComponent>().Build();
            _cargoViewStash = World.GetStash<CargoViewComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var request in _cargoUpgradeRequest.Consume())
            {
                UpgradeCargoView();
            }
        }
        
        private void UpgradeCargoView()
        {
            foreach (var entity in _filter)
            {
                ref var c = ref _cargoViewStash.Get(entity);
                c.ViewLink.UpdateValues();
            }
        }
    }
}
