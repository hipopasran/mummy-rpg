using Scellecs.Morpeh;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace Secret
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct CargoFullComponent : IComponent
    {
        public CargoFullView ProviderLink;
        public float TimeToShow;
        public float CurrentTime;
        public GameObject GameObjectLink;
    }
}
