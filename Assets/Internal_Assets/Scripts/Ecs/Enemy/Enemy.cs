using Scellecs.Morpeh;
using Scellecs.Morpeh.Providers;
using Unity.IL2CPP.CompilerServices;
using UnityEngine;

namespace Secret
{
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public class Enemy : MonoProvider<EnemyComponent>
    {
        public void Setup(Entity spawner, Vector3 spawnerPos, float walkRadius, int agentPriority)
        {
            ref var c = ref Stash.Get(Entity);
            c.Spawner = spawner;
            c.SpawnerPosition = spawnerPos;
            c.SpawnerRadius = walkRadius;
            c.AgentPriority = agentPriority;

            c.Agent.avoidancePriority = agentPriority;
        }
    }
}
