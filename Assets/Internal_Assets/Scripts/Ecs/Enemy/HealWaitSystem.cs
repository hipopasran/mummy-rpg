using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class HealWaitSystem : ISystem
    {
        private Filter _filter;
        private Stash<EnemyComponent> _enemyStash;
        private Stash<HealWaitComponent> _healWaitStash;

        public World World { get; set; }
        public void Dispose()
        {
           
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<EnemyComponent>().With<HealWaitComponent>().Build();
            this._enemyStash = this.World.GetStash<EnemyComponent>();
            this._healWaitStash = this.World.GetStash<HealWaitComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var enemy = ref _enemyStash.Get(entity);
                ref var healWait = ref _healWaitStash.Get(entity);
                
                HealWaitTimer(ref enemy, ref healWait, deltaTime);
            }
        }

        private void HealWaitTimer(ref EnemyComponent enemy, ref HealWaitComponent healWait, float deltaTime)
        {
            healWait.TimeToWait -= deltaTime;

            if (healWait.TimeToWait <= 0)
            {
                Object.Destroy(healWait.FilterLink);
                enemy.Root.gameObject.AddComponent<ActiveHeal>();
            }
        }
    }
}
