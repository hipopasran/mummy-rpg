using System.Collections.Generic;
using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Secret
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class TentacleAttackFilterProvider : MonoProvider<TentacleAttackFilter>
    {
        public void Setup(Transform enemy)
        {
            ref var c = ref Stash.Get(Entity);
            c.EnemyToAttack = enemy;
        }
    }
}
