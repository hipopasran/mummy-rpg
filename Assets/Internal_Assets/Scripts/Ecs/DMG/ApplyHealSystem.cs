using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class ApplyHealSystem : ISystem
    {
        private Filter _filter;
        private Stash<EnemyComponent> _enemyStash;
        private Stash<ActiveHealComponent> _healStash;
        private Stash<HealthBarComponent> _healthBarStash;
        
        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<EnemyComponent>().With<ActiveHealComponent>().With<HealthBarComponent>().Build();
            this._enemyStash = this.World.GetStash<EnemyComponent>();
            this._healStash = this.World.GetStash<ActiveHealComponent>();
            this._healthBarStash = this.World.GetStash<HealthBarComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var enemy = ref _enemyStash.Get(entity);
                ref var heal = ref _healStash.Get(entity);
                ref var healthBar = ref _healthBarStash.Get(entity);

                ApplyHeal(ref enemy, ref heal, ref healthBar, deltaTime);
            }
        }

        private void ApplyHeal(ref EnemyComponent enemy, ref ActiveHealComponent heal, ref HealthBarComponent healthBar, float deltaTime)
        {
            enemy.CurrentHealth += 1f;

            if (enemy.CurrentHealth >= enemy.StartHealth)
            {
                enemy.CurrentHealth = enemy.StartHealth;
                if (healthBar.Root.gameObject.activeSelf)
                {
                    healthBar.Root.gameObject.SetActive(false);
                }
                var p = enemy.Root.gameObject.GetComponent<ActiveHeal>();
                Object.Destroy(p);
            }
        }
    }
}
