using Scellecs.Morpeh;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.AI;

namespace Secret
{
    public class EnemyWalkSystem : ISystem
    {
        private Filter _filter;
        private Stash<EnemyComponent> _enemyStash;
        private Stash<EnemyWalkComponent> _enemyWalkStash;
        
        public World World { get; set; }
        public void Dispose()
        {
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<EnemyComponent>().With<EnemyWalkComponent>().Build();
            this._enemyStash = this.World.GetStash<EnemyComponent>();
            this._enemyWalkStash = this.World.GetStash<EnemyWalkComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var enemy = ref _enemyStash.Get(entity);
                ref var enemyWalk = ref _enemyWalkStash.Get(entity);
                
                Move(ref enemy, ref enemyWalk);
            }
        }

        private void Move(ref EnemyComponent enemy, ref EnemyWalkComponent enemyWalk)
        {
            var newPoint = GetNearestNavMeshPoint(enemy.SpawnerPosition, enemy.SpawnerRadius);
            enemy.Agent.SetDestination(newPoint);
            Object.Destroy(enemyWalk.FilterLink);
            var walkProgress = enemy.Root.gameObject.AddComponent<EnemyWalkInProgressFilter>();
            walkProgress.SetPoint(newPoint);
            if (enemy.animator)
            {
                if (!enemy.animator.GetBool("Walking"))
                {
                    enemy.animator.SetBool("Walking", true);
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