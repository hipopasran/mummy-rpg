using System.Collections.Generic;
using Obi;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Secret
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class EnemyFlyCargoProvider : MonoProvider<EnemyFlyCargoComponent>
    {
        public void Setup()
        {
            ref var c = ref Stash.Get(Entity);
            c.Root = transform;
            c.StartDistance = Vector3.Distance(transform.position, PlayerLiveStats.Instance.PlayerLink.position);
        }
    }
}
