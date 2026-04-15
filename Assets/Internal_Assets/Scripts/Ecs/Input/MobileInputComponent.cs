using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace Secret
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]

    public struct MobileInputComponent : IComponent
    {
        public Vector3 StartPos;
        public Vector3 Direction;
        public Vector3 Movement;
        public Input_Actions Input;
    }
}
