using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.AI;

namespace Secret
{
    public class EnemyInitSpawnSystem : ISystem
    {
        private Filter _filter;
        private Stash<SpawnerComponent> _spawnerStash;
        private Stash<SpawnerInitStateComponent> _spawnerInitStash;

        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<SpawnerComponent>().With<SpawnerInitStateComponent>().Build();
            this._spawnerStash = this.World.GetStash<SpawnerComponent>();
            this._spawnerInitStash = this.World.GetStash<SpawnerInitStateComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in this._filter)
            {
                ref var spawner = ref _spawnerStash.Get(entity);
                ref var spawnerInit = ref _spawnerInitStash.Get(entity);

                SetupSpawner(ref spawner);
                Spawn(ref spawner, ref spawnerInit, deltaTime, entity);
            }
        }

        private void SetupSpawner(ref SpawnerComponent spawner)
        {
            spawner.Radius = spawner.Setup.Radius;
            spawner.EnemyCount = spawner.Setup.EnemyCount;
            spawner.EnemyPrefab = spawner.Setup.EnemyPrefab;
        }

        private void Spawn(ref SpawnerComponent spawner, ref SpawnerInitStateComponent spawnerInit, float deltaTime, Entity entity)
        {
            if (spawner.ExistEnemyCount < spawner.EnemyCount)
            {
                for (int i = 0; i < spawner.EnemyCount; i++)
                {
                    spawner.SpawnerLink.SpawnEnemy(ref spawner);
                }
                
                Object.Destroy(spawnerInit.FilterLink);
            }
        }
    }
}
