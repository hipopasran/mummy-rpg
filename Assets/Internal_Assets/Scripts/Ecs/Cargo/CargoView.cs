using System;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Secret
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class CargoView : MonoProvider<CargoViewComponent>
    {
        public void UpdateValues()
        {
            ref var c = ref Stash.Get(Entity);
            c.CargoValueText.text = "CARGO(" + PlayerStats.Instance.CargoCurrent + "/" + PlayerStats.Instance.CargoMax + ")";
            c.FillBar.sizeDelta =
                new Vector2(c.FillBarMaxValue * ((float)PlayerStats.Instance.CargoCurrent / (float)PlayerStats.Instance.CargoMax),
                    c.FillBar.sizeDelta.y);
        }
    }
}
