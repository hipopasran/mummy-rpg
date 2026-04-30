using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace Secret
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct SpawnerComponent : IComponent
    {
        public GameObject SpawnerGameObject;
        public Spawner SpawnerLink;
        public EnemySetupForSpawner Setup;
        public Transform Position;
        public float TimeToRespawn;
        public float Timer;
        
       [HideInInspector] public float Radius;
       [HideInInspector] public int EnemyCount;
        public int ExistEnemyCount;
       [HideInInspector] public Enemy EnemyPrefab;
    }
}
