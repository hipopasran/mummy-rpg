using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Secret
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class EnemyWaitIdleFilter : MonoProvider<EnemyWaitIdleComponent>
    {
        public void Setup(float timeToWait)
        {
            ref var c = ref Stash.Get(Entity);
            c.FilterLink = this;
            c.TimeToWait = timeToWait;
            c.CurrentWaitTime = timeToWait;
        }
    }
}
