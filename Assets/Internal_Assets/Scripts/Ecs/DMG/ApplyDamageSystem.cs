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
            // Check Cargo have place
            if (!PlayerStats.Instance.IsHaveCargoPlace(enemy.Cargo))
            {
                var p = enemy.Root.gameObject.GetComponent<ActiveDamage>();
                Object.Destroy(p);
                var hw = enemy.Root.gameObject.AddComponent<HealWait>();
                hw.Setup(2f);
                return;
            }
            
            enemy.CurrentHealth -= 20f * deltaTime;

            if (enemy.CurrentHealth <= 0)
            {
                // Send exp and Resources request
                enemy.ProviderLink.SendExpRequest();
                enemy.ProviderLink.SendCargoRequest();
                //
                
                var p = enemy.Root.gameObject.GetComponent<ActiveDamage>();
                Object.Destroy(p);
                
                enemy.SpawerLink.EnemyDead();
                enemy.Root.gameObject.SetActive(false);
            }
        }
    }
}
