using Scellecs.Morpeh;
using TMPro;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.UI;

namespace Secret
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct HealthBarComponent : IComponent
    {
        public Transform Root;
        public Transform Background;
        public TextMeshProUGUI PercentText;
        public Image Fill;
        public float WidthMax;
    }
}
