using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;

namespace Secret
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class Spawner : MonoProvider<SpawnerComponent>
    {
        public void EnemyDead()
        {
            ref var c = ref Stash.Get(Entity);
            c.ExistEnemyCount -= 1;
        }
    }
}
