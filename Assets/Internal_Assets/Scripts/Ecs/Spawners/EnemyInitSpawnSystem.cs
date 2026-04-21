using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.AI;

namespace Secret
{
    public class EnemyInitSpawnSystem : ISystem
    {
        private Filter _filter;
        private Stash<SpawnerComponent> _spawnerStash;

        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<SpawnerComponent>().Build();
            this._spawnerStash = this.World.GetStash<SpawnerComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in this._filter)
            {
                ref var spawner = ref _spawnerStash.Get(entity);

                SetupSpawner(ref spawner);
                Spawn(ref spawner, deltaTime, entity);
            }
        }

        private void SetupSpawner(ref SpawnerComponent spawner)
        {
            spawner.Radius = spawner.Setup.Radius;
            spawner.EnemyCount = spawner.Setup.EnemyCount;
            spawner.EnemyPrefab = spawner.Setup.EnemyPrefab;
        }

        private void Spawn(ref SpawnerComponent spawner, float deltaTime, Entity entity)
        {
            if (spawner.ExistEnemyCount < spawner.EnemyCount)
            {
                for (int i = 0; i < spawner.EnemyCount; i++)
                {
                    var spawnPos = GetNearestNavMeshPoint(spawner.Position.position, spawner.Radius);
                    var newEnemy = GameObject.Instantiate(spawner.EnemyPrefab, spawnPos, Quaternion.identity);
                    newEnemy.transform.SetParent(spawner.Position);
                    newEnemy.Setup(entity, spawner.Position.position, spawner.Radius, 99 - spawner.ExistEnemyCount);
                    newEnemy.SetWalkParams(spawner.Setup.WaitIdleMinTime, spawner.Setup.WaitIdleMaxTime, spawner.Setup.WalkSpeed, spawner.Setup.RunSpeed);
                    var walkFilter = newEnemy.gameObject.AddComponent<EnemyWalkFilter>();
                    walkFilter.Setup();

                    spawner.ExistEnemyCount += 1;
                }
            }
        }
        
        private Vector3 GetNearestNavMeshPoint(Vector3 sourcePosition, float range)
        {
            float angle = Random.value * Mathf.PI * 2;
            float distance = range * Mathf.Sqrt(Random.value); 
            Vector2 point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            Vector3 randomPos = sourcePosition + new Vector3(point.x,0,point.y);

            NavMeshHit hit;
            if (NavMesh.SamplePosition(randomPos, out hit, 1.0f, NavMesh.AllAreas))
            {
                return hit.position;
            }
            return sourcePosition;
        }
    }
}
