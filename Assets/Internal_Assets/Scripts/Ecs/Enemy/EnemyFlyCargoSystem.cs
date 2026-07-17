using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class EnemyFlyCargoSystem : ISystem
    {
        private Filter _filter;
        private Stash<EnemyComponent> _enemyStash;
        private Stash<EnemyFlyCargoComponent> _flyCargoStash;
        
        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<EnemyComponent>().With<EnemyFlyCargoComponent>().Build();
            _enemyStash = World.GetStash<EnemyComponent>();
            _flyCargoStash = World.GetStash<EnemyFlyCargoComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var enemy = ref _enemyStash.Get(entity);
                ref var flyCargo = ref _flyCargoStash.Get(entity);
                
                ScaleSize(ref enemy, ref flyCargo);
            }
        }

        private void ScaleSize(ref EnemyComponent enemy, ref EnemyFlyCargoComponent flyCargo)
        {
            if (enemy.Root.localScale.x > 0.5f)
            {
                var distance = Vector3.Distance(enemy.Root.position, PlayerLiveStats.Instance.PlayerLink.position);
                enemy.Root.localScale = enemy.Root.localScale  * distance/flyCargo.StartDistance;
            }
        }
    }
}
