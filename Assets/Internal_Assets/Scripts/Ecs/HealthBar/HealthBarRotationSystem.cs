using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class HealthBarRotationSystem : ISystem
    {
        private Filter _filter;
        private Stash<HealthBarComponent> _barStash;

        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<HealthBarComponent>().Build();
            this._barStash = this.World.GetStash<HealthBarComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var bar = ref _barStash.Get(entity);
                BarLookToCamera(ref bar);
            }
        }

        private void BarLookToCamera(ref HealthBarComponent bar)
        {
            bar.Root.LookAt(bar.Root.position + Camera.main.transform.rotation * Vector3.back, Camera.main.transform.rotation * Vector3.up);
        }
    }
}
