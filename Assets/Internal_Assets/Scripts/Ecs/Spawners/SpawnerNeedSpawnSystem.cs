using UnityEngine;
using Scellecs.Morpeh;

namespace Secret
{
    public class SpawnerNeedSpawnSystem : ISystem
    {
        private Filter _filter;
        private Stash<SpawnerComponent> _spawnerStash;
        private Stash<SpawnerNeedSpawnComponent> _needSpawnStash;

        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<SpawnerComponent>().With<SpawnerNeedSpawnComponent>().Build();
            this._spawnerStash = this.World.GetStash<SpawnerComponent>();
            this._needSpawnStash = this.World.GetStash<SpawnerNeedSpawnComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var spawner = ref _spawnerStash.Get(entity);
                ref var needSpawn = ref _needSpawnStash.Get(entity);
                
                CheckForSpawnCondition(ref spawner, ref needSpawn, deltaTime);
            }
        }

        private void CheckForSpawnCondition(ref SpawnerComponent spawner, ref SpawnerNeedSpawnComponent needSpawn,
            float deltaTime)
        {
            spawner.Timer += deltaTime;
            if (spawner.Timer >= spawner.TimeToRespawn)
            {
                spawner.SpawnerLink.SpawnEnemy(ref spawner);
                spawner.Timer = 0f;

                if (spawner.ExistEnemyCount >= spawner.EnemyCount)
                {
                    Object.Destroy(needSpawn.FilterLink);
                }
            }
        }
    }
}
