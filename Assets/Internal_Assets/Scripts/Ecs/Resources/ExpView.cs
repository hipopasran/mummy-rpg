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
    public class ExpView : MonoProvider<ExpViewComponent>
    {
        public void UpdateValues()
        {
            ref var c = ref Stash.Get(Entity);
            c.LevelNumber.text = "1";
            c.ExpValueText.text = PlayerLiveStats.Instance.ExpCurrent + "/" + PlayerLiveStats.Instance.ExpMax;
            c.FillBar.sizeDelta =
                new Vector2(c.FillBarMaxValue * (PlayerLiveStats.Instance.ExpCurrent / PlayerLiveStats.Instance.ExpMax),
                    c.FillBar.sizeDelta.y);
        }
    }
}
