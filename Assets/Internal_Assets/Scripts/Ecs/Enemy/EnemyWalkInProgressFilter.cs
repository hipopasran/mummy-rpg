using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Secret
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class EnemyWalkInProgressFilter : MonoProvider<EnemyWalkInProgressComponent>
    {
        public void SetPoint(Vector3 destination)
        {
            ref var c = ref Stash.Get(Entity);
            c.WalkPosition = destination;
            c.FilterLink = this;
        }
    }
}
