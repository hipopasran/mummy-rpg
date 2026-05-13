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
        private Request<ExpRequest> _expRequest;
        
        protected override void Initialize()
        {
            _expRequest = World.Default.GetRequest<ExpRequest>();
        }
        
        public void Setup(Spawner spawnerLink, Entity spawner, Vector3 spawnerPos, float walkRadius, int agentPriority)
        {
            ref var c = ref Stash.Get(Entity);
            c.Spawner = spawner;
            c.SpawnerPosition = spawnerPos;
            c.SpawnerRadius = walkRadius;
            c.AgentPriority = agentPriority;
            c.SpawerLink = spawnerLink;

            c.Agent.avoidancePriority = agentPriority;
        }

        public void SetWalkParams(float minIdleTime, float maxIdleTime, float walkSpeed, float runSpeed)
        {
            ref var c = ref Stash.Get(Entity);
            c.WaitIdleMinTime = minIdleTime;
            c.WaitIdleMaxTime = maxIdleTime;
            c.WalkSpeed = walkSpeed;
            c.RunSpeed = runSpeed;

            c.Agent.speed = walkSpeed;
        }

        public void SendExpRequest()
        {
            ref var c = ref Stash.Get(Entity);
            _expRequest.Publish(new ExpRequest
            {
                //TODO: Possible null?
                TargetEntity = Entity,
                Exp = c.Exp
            });
        }
    }
}
