using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class FarmSystem : ISystem
    {
        private Filter _filter;
        private Stash<FarmComponent> _farmStash;
        
        public World World { get; set; }
        
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<FarmComponent>().Build();
            _farmStash = World.GetStash<FarmComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var farm = ref _farmStash.Get(entity);
                
                UpdateValues(ref farm, deltaTime);
            }
        }

        private void UpdateValues(ref FarmComponent farm, float deltaTime)
        {
            if(farm.FarmManager.CurrentValue >= farm.FarmManager.Capacity) return;
            
            farm.FarmManager.AddValue(farm.FarmManager.Income * deltaTime);
        }
    }
}
