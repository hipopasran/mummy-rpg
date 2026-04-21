using TriInspector;
using UnityEngine;

namespace Secret
{
    [CreateAssetMenu(fileName = "SetupForSpawner", menuName = "Data/SetupForSpawner", order = 1)]
    public class EnemySetupForSpawner : ScriptableObject
    {
        [Title("Spawner set")]
        public int EnemyCount;
        public Enemy EnemyPrefab;
        public float Radius;

        [Title("Wait Idle Time")]
        public float WaitIdleMinTime;
        public float WaitIdleMaxTime;

        [Title("Movement Values")] 
        public float WalkSpeed;
        public float RunSpeed;
        
        [Title("Enemy Values")]
        public float EnemyHealth;
        public float EnemyExp;
    }
}
