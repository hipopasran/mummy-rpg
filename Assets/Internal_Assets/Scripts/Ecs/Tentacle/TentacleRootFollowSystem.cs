using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class TentacleRootFollowSystem : ISystem
    {
        private Filter _filter;
        private Stash<TentacleRootComponent> _tentacleRootStash;

        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<TentacleRootComponent>().Build();
            this._tentacleRootStash = this.World.GetStash<TentacleRootComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var tentacleRoot = ref _tentacleRootStash.Get(entity);
                
                Follow(ref tentacleRoot);
            }
        }

        public void Follow(ref TentacleRootComponent tentacleRoot)
        {
            tentacleRoot.Root.position = tentacleRoot.ObjectToFollow.position;
        }
    }
}
