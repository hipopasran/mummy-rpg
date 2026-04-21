using Scellecs.Morpeh;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Secret
{
    public class EnemyWalkInProgressSystem : ISystem
    {
        private Filter _filter;
        private Stash<EnemyComponent> _enemyStash;
        private Stash<EnemyWalkInProgressComponent> _enemyWalkProgressStash;
        
        public World World { get; set; }
        public void Dispose()
        {
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<EnemyComponent>().With<EnemyWalkInProgressComponent>().Build();
            this._enemyStash = this.World.GetStash<EnemyComponent>();
            this._enemyWalkProgressStash = this.World.GetStash<EnemyWalkInProgressComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var enemy = ref _enemyStash.Get(entity);
                ref var walkProgress = ref _enemyWalkProgressStash.Get(entity);
                
                CheckDestination(ref enemy, ref walkProgress);
            }
        }

        private void CheckDestination(ref EnemyComponent enemy, ref  EnemyWalkInProgressComponent walkProgress)
        {
            if (Vector3.Distance(enemy.Root.position, walkProgress.WalkPosition) < 1.0f)
            {
                Object.Destroy(walkProgress.FilterLink);
                var idleFilter = enemy.Root.gameObject.AddComponent<EnemyWaitIdleFilter>();
                idleFilter.Setup(Random.Range(enemy.WaitIdleMinTime, enemy.WaitIdleMaxTime));
            }
        }
    }
}
