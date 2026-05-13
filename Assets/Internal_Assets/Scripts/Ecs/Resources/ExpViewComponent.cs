using Scellecs.Morpeh;
using TMPro;
using TriInspector;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace Secret
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct ExpViewComponent : IComponent
    {
        public ExpView ViewLink;
        public TextMeshProUGUI LevelNumber;
        public float FillBarMaxValue;
        public RectTransform FillBar;
        public TextMeshProUGUI ExpValueText;
    }
}
