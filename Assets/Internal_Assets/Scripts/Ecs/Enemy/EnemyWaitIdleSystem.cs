using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.AI;

namespace Secret
{
    public class EnemyWaitIdleSystem : ISystem
    {
        private Filter _filter;
        private Stash<EnemyComponent> _enemyStash;
        private Stash<EnemyWaitIdleComponent> _enemyWaitIdleStash;
        public World World { get; set; }
        
        public void Dispose()
        {
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<EnemyComponent>().With<EnemyWaitIdleComponent>().Build();
            this._enemyStash = this.World.GetStash<EnemyComponent>();
            this._enemyWaitIdleStash = this.World.GetStash<EnemyWaitIdleComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var enemy = ref _enemyStash.Get(entity);
                ref var waitIdle = ref _enemyWaitIdleStash.Get(entity);
                
                WaitInIdle(ref enemy, ref waitIdle, deltaTime);
            }
        }

        private void WaitInIdle(ref EnemyComponent enemy, ref EnemyWaitIdleComponent enemyWaitIdle, float deltaTime)
        {
            enemyWaitIdle.CurrentWaitTime -= deltaTime;

            if (enemy.animator)
            {
                if (enemy.animator.GetBool("Walking"))
                {
                    enemy.animator.SetBool("Walking", false);
                }
            }

            if (enemyWaitIdle.CurrentWaitTime <= 0)
            {
                Object.Destroy(enemyWaitIdle.FilterLink);
                var walkFilter = enemy.Root.gameObject.AddComponent<EnemyWalkFilter>();
                enemy.Agent.isStopped = false;
                walkFilter.Setup();
            }
        }
    }
}
