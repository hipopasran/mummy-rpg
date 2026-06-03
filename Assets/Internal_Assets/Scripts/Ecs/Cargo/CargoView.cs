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

            if (PlayerLiveStats.Instance.IsCargoFull)
            {
                c.CargoValueText.text = "CARGO IS FULL";
                c.FillBar.sizeDelta =
                    new Vector2(c.FillBarMaxValue, c.FillBar.sizeDelta.y);
                c.CargoFillImage.color = c.CargoFullColor;
            }
            else
            {
                c.CargoValueText.text = "CARGO(" + PlayerLiveStats.Instance.CargoCurrent + "/" + PlayerLiveStats.Instance.CargoMax + ")";
                c.FillBar.sizeDelta =
                    new Vector2(c.FillBarMaxValue * ((float)PlayerLiveStats.Instance.CargoCurrent / (float)PlayerLiveStats.Instance.CargoMax),
                        c.FillBar.sizeDelta.y);
                c.CargoFillImage.color = c.CargoHavePlaceColor;
            }
        }
    }
}
