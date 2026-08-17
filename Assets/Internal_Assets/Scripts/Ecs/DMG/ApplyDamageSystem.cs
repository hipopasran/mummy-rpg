using Scellecs.Morpeh;
using Unity.VisualScripting;
using UnityEngine;

namespace Secret
{
    public class ApplyDamageSystem : ISystem
    {
        private Filter _filter;
        private Stash<EnemyComponent> _enemyStash;
        private Stash<ActiveDamageComponent> _damageStash;
        private Stash<HealthBarComponent> _healthBarStash;

        public World World { get; set; }
        public void Dispose()
        {
           
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<EnemyComponent>().With<ActiveDamageComponent>().With<HealthBarComponent>().Build();
            this._enemyStash = this.World.GetStash<EnemyComponent>();
            this._damageStash = this.World.GetStash<ActiveDamageComponent>();
            this._healthBarStash = this.World.GetStash<HealthBarComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var enemy = ref _enemyStash.Get(entity);
                ref var dmg = ref _damageStash.Get(entity);
                ref var healthBar = ref _healthBarStash.Get(entity);
                if (!healthBar.Root.gameObject.activeSelf)
                {
                    healthBar.Root.gameObject.SetActive(true);
                }

                ApplyDamage(ref enemy, ref dmg, deltaTime);
            }
        }

        private void ApplyDamage(ref EnemyComponent enemy, ref ActiveDamageComponent dmg, float deltaTime)
        {
            // Check Cargo have place
            if (!PlayerLiveStats.Instance.IsHaveCargoPlace(enemy.Cargo))
            {
                var p = enemy.Root.gameObject.GetComponent<ActiveDamage>();
                Object.Destroy(p);
                var hw = enemy.Root.gameObject.AddComponent<HealWait>();
                hw.Setup(2f);
                
                enemy.ProviderLink.SendCargoFullRequest();
                return;
            }
            
            // TODO: Подключить дамаг из парамтеров игрока
            enemy.CurrentHealth -= 14f * deltaTime;

            if (enemy.CurrentHealth <= 0)
            {
                if (enemy.Root.TryGetComponent(out EnemyWalkFilter walk))
                {
                    Object.Destroy(walk);
                }
                if (enemy.Root.TryGetComponent(out EnemyWalkInProgressFilter walkProgres))
                {
                    Object.Destroy(walkProgres);
                }
                if (enemy.Root.TryGetComponent(out EnemyWaitIdleFilter idle))
                {
                    Object.Destroy(idle);
                }

                if (enemy.Root.TryGetComponent(out HealthBarProvider health))
                {
                    health.EnemyDead();
                    Object.Destroy(health);
                }

                enemy.collider.enabled = false;
                enemy.Agent.enabled = false;
                enemy.Root.parent = enemy.TentackleFromAttack;
                enemy.TentackleFromAttack.parent.AddComponent<TentacleHomeFilterProvider>();

                // Send exp and Resources request
                // enemy.ProviderLink.SendExpRequest();
                // enemy.ProviderLink.SendCargoRequest();
                //
                
                var p = enemy.Root.gameObject.GetComponent<ActiveDamage>();
                Object.Destroy(p);

                var flyCargo = enemy.Root.AddComponent<EnemyFlyCargoProvider>();
                flyCargo.Setup();
                
                enemy.SpawerLink.EnemyDead();
                // enemy.Root.gameObject.SetActive(false);
            }
        }
    }
}
