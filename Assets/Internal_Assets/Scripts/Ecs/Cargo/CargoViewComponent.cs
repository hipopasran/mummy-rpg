using Scellecs.Morpeh;
using TMPro;
using TriInspector;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;
using UnityEngine.UI;

namespace Secret
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct CargoViewComponent : IComponent
    {
        public CargoView ViewLink;
        public float FillBarMaxValue;
        public RectTransform FillBar;
        public TextMeshProUGUI CargoValueText;
        public Color CargoHavePlaceColor;
        public Color CargoFullColor;
        public Image CargoFillImage;
    }
}
