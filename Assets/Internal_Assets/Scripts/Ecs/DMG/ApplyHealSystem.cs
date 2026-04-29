using Scellecs.Morpeh;
using Unity.VisualScripting;
using UnityEngine;

namespace Secret
{
    public class ApplyHealSystem : ISystem
    {
        private Filter _filter;
        private Stash<EnemyComponent> _enemyStash;
        private Stash<ActiveHealComponent> _healStash;
        
        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<EnemyComponent>().With<ActiveHealComponent>().Build();
            this._enemyStash = this.World.GetStash<EnemyComponent>();
            this._healStash = this.World.GetStash<ActiveHealComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var enemy = ref _enemyStash.Get(entity);
                ref var heal = ref _healStash.Get(entity);
                
                ApplyHeal(ref enemy, ref heal, deltaTime);
            }
        }

        private void ApplyHeal(ref EnemyComponent enemy, ref ActiveHealComponent heal, float deltaTime)
        {
            enemy.CurrentHealth += 1f;

            if (enemy.CurrentHealth >= enemy.StartHealth)
            {
                enemy.CurrentHealth = enemy.StartHealth;
                var p = enemy.Root.gameObject.GetComponent<ActiveHeal>();
                Object.Destroy(p);
            }
        }
    }
}
