using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class CargoClearSystem : ISystem
    {
        private Filter _filter;
        private Stash<CargoViewComponent> _cargoViewStash;
        private Request<CargoClearRequest> _cargoClearRequest;
        
        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            _cargoClearRequest = World.GetRequest<CargoClearRequest>();
            _filter = World.Filter.With<CargoViewComponent>().Build();
            _cargoViewStash = World.GetStash<CargoViewComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var request in _cargoClearRequest.Consume())
            {
                ClearCargoView();
            }
        }

        private void ClearCargoView()
        {
            foreach (var entity in _filter)
            {
                ref var c = ref _cargoViewStash.Get(entity);
                c.ViewLink.ResetCargo();
            }
        }
    }
}
