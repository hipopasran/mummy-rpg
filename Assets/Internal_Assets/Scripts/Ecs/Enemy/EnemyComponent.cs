using Scellecs.Morpeh;
using TriInspector;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.AI;

namespace Secret
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct EnemyComponent : IComponent
    {
        [Title("Links")]
        public Transform Root;
        public NavMeshAgent Agent;

        [Title("Params")] 
        public float WaitIdleMinTime;
        public float WaitIdleMaxTime;

        public float WalkSpeed;
        public float RunSpeed;
        
        [Title("Debug")]
        public int AgentPriority;
        public Entity Spawner;
        public Vector3 SpawnerPosition;
        public float SpawnerRadius;
    }
}
