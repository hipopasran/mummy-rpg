using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class TentacleHomeSystem : ISystem
    {
        private Filter _filter;
        private Stash<TentacleComponent> _tentacleStash;
        private Stash<TentacleHomeFilter> _tentacleHomeStash;
        
        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<TentacleComponent>().With<TentacleHomeFilter>().Build();
            _tentacleStash = World.GetStash<TentacleComponent>();
            _tentacleHomeStash = World.GetStash<TentacleHomeFilter>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var tentacle = ref _tentacleStash.Get(entity);
                ref var tentacleHome = ref _tentacleHomeStash.Get(entity);
                
                Home(ref tentacle, ref tentacleHome);
            }
        }

        public void Home(ref TentacleComponent tentacle, ref TentacleHomeFilter tentacleHome)
        {
            if (Vector3.Distance(tentacle.Root.position, tentacle.Home.position) > 0.1f)
            {
                float step = 20f * Time.deltaTime;
                tentacle.Root.position =
                    Vector3.MoveTowards(tentacle.Root.position, tentacle.Home.position, step);
            }
            else
            {
                tentacle.Provider.SetReady();
            }
        }
    }
}
