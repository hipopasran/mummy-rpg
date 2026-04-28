using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class HealthBarValueViewSystem : ISystem
    {
        private Filter _filter;
        private Stash<EnemyComponent> _enemyStash;
        private Stash<HealthBarComponent> _barStash;

        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<EnemyComponent>().With<HealthBarComponent>().Build();
            this._enemyStash = this.World.GetStash<EnemyComponent>();
            this._barStash = this.World.GetStash<HealthBarComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var enemy = ref _enemyStash.Get(entity);
                ref var bar = ref _barStash.Get(entity);
                
                UpdateHealthBarView(ref enemy, ref bar, deltaTime);
            }
        }

        private void UpdateHealthBarView(ref EnemyComponent enemy, ref HealthBarComponent bar, float deltaTime)
        {
            var healthPercent = enemy.CurrentHealth / enemy.StartHealth;
            bar.PercentText.text = $"{(int)(healthPercent*100f)}%";

            bar.Fill.rectTransform.sizeDelta = new Vector2(bar.WidthMax * healthPercent, bar.Fill.rectTransform.sizeDelta.y);
            
            // Test DMG
            // enemy.CurrentHealth -= 10f * deltaTime;
        }
    }
}
