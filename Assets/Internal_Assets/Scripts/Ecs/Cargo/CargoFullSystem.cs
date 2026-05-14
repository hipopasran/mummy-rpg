using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class CargoFullSystem : ISystem
    {
        private Filter _filter;
        private Stash<CargoFullComponent> _cargoFullStash;
        private Request<CargoFullRequest> _cargoFullRequest;

        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            _cargoFullRequest = World.GetRequest<CargoFullRequest>();
            _filter = World.Filter.With<CargoFullComponent>().Build();
            _cargoFullStash = World.GetStash<CargoFullComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var request in _cargoFullRequest.Consume())
            {
                ApplyCargoFull();
            }

            foreach (var entity in _filter)
            {
                UpdateTimer(entity, deltaTime);
            }
        }

        private void ApplyCargoFull()
        {
            foreach (var entity in _filter)
            {
                ref var c = ref _cargoFullStash.Get(entity);
                c.ProviderLink.ResetTimer();
            }
        }

        private void UpdateTimer(Entity entity, float deltaTime)
        {
            ref var c = ref _cargoFullStash.Get(entity);
            if (c.GameObjectLink.activeSelf)
            {
                c.CurrentTime -= deltaTime;

                if (c.CurrentTime <= 0)
                {
                    c.GameObjectLink.SetActive(false);
                }
            }
        }
    }
}
