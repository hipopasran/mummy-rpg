using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.AI;

namespace Secret
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct PlayerMovementComponent : IComponent
    {
        public float WalkSpeed;
        public Transform Transform;
        public NavMeshAgent Agent;
    }
}
