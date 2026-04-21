using Scellecs.Morpeh;
using TriInspector;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace Secret
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct SpawnerInitStateComponent : IComponent
    {
        public SpawnerInitStateFilter FilterLink;
    }
}
