using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Secret
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class Spawner : MonoProvider<SpawnerComponent>
    {
        public void EnemyDead()
        {
            ref var c = ref Stash.Get(Entity);
            c.ExistEnemyCount -= 1;

            if (gameObject.TryGetComponent(out SpawnerNeedSpawnFilter needSpawnFilter))
            {
                return;
            }
            else
            {
                var filter = gameObject.AddComponent<SpawnerNeedSpawnFilter>();
                filter.Setup();
            }
        }

        public void SpawnEnemy(ref SpawnerComponent spawner)
        {
            var spawnPos = GetNearestNavMeshPoint(spawner.Position.position, spawner.Radius);
            var newEnemy = GameObject.Instantiate(spawner.EnemyPrefab, spawnPos, Quaternion.identity);
            newEnemy.transform.SetParent(spawner.Position);
            newEnemy.Setup(spawner.SpawnerLink, Entity, spawner.Position.position, spawner.Radius, 99 - spawner.ExistEnemyCount);
            newEnemy.SetWalkParams(spawner.Setup.WaitIdleMinTime, spawner.Setup.WaitIdleMaxTime, spawner.Setup.WalkSpeed, spawner.Setup.RunSpeed);
            var walkFilter = newEnemy.gameObject.AddComponent<EnemyWalkFilter>();
            walkFilter.Setup();

            spawner.ExistEnemyCount += 1;
        }
        
        private Vector3 GetNearestNavMeshPoint(Vector3 sourcePosition, float range)
        {
            float angle = Random.value * Mathf.PI * 2;
            float distance = range * Mathf.Sqrt(Random.value); 
            Vector2 point = new Vector2(Mathf.Cos(angle), Mathf.Sin(angle)) * distance;
            Vector3 randomPos = sourcePosition + new Vector3(point.x,0,point.y);

            UnityEngine.AI.NavMeshHit hit;
            if (UnityEngine.AI.NavMesh.SamplePosition(randomPos, out hit, 1.0f, UnityEngine.AI.NavMesh.AllAreas))
            {
                return hit.position;
            }
            return sourcePosition;
        }
    }
}
