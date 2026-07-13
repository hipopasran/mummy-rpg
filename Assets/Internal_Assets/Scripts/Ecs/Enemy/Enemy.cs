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
    public class Enemy : MonoProvider<EnemyComponent>
    {
        private Request<ExpRequest> _expRequest;
        private Request<CargoRequest> _cargoRequest;
        private Request<CargoFullRequest> _cargoFullRequest;

        protected override void Initialize()
        {
            _expRequest = World.Default.GetRequest<ExpRequest>();
            _cargoRequest = World.Default.GetRequest<CargoRequest>();
            _cargoFullRequest = World.Default.GetRequest<CargoFullRequest>();
        }

        public bool GetEnoughCargo()
        {
            ref var c = ref Stash.Get(Entity);
            if (PlayerLiveStats.Instance.CargoCurrent + c.Cargo <= PlayerLiveStats.Instance.CargoMax)
            {
                return true;
            }

            return false;
        }

        public List<ResourcePack> GetResources()
        {
            ref var c = ref Stash.Get(Entity);
            return c.Resources;
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

        public void SendCargoRequest()
        {
            ref var c = ref Stash.Get(Entity);
            _cargoRequest.Publish(new CargoRequest
            {
                //TODO: Possible null?
                TargetEntity = Entity,
                Cargo = c.Cargo,
                Resource = c.Resources
            });
        }

        public void SendCargoFullRequest()
        {
            _cargoFullRequest.Publish(new CargoFullRequest());
        }
    }
}
