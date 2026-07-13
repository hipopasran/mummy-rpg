using System.Collections.Generic;
using Obi;
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
    public struct TentacleComponent : IComponent
    {
        [Title("Links")] 
        public TentacleProvider Provider;
        public Transform Root;
        public Transform Home;
    }
}
