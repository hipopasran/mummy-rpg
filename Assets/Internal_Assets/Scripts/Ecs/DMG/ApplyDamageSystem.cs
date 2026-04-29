using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class ApplyDamageSystem : ISystem
    {
        private Filter _filter;
        private Stash<EnemyComponent> _enemyStash;
        private Stash<ActiveDamageComponent> _damageStash;

        public World World { get; set; }
        public void Dispose()
        {
           
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<EnemyComponent>().With<ActiveDamageComponent>().Build();
            this._enemyStash = this.World.GetStash<EnemyComponent>();
            this._damageStash = this.World.GetStash<ActiveDamageComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var enemy = ref _enemyStash.Get(entity);
                ref var dmg = ref _damageStash.Get(entity);

                ApplyDamage(ref enemy, ref dmg, deltaTime);
            }
        }

        private void ApplyDamage(ref EnemyComponent enemy, ref ActiveDamageComponent dmg, float deltaTime)
        {
            enemy.CurrentHealth -= 10f * deltaTime;

            if (enemy.CurrentHealth <= 0)
            {
                var p = enemy.Root.gameObject.GetComponent<ActiveDamage>();
                Object.Destroy(p);
            }
        }
    }
}
