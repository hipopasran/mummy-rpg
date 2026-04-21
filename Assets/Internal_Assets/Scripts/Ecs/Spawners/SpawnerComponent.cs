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
        public Transform Position;
        public float Radius;
        public int EnemyCount;
        public int ExistEnemyCount;
        public GameObject EnemyPrefab;
    }
}
