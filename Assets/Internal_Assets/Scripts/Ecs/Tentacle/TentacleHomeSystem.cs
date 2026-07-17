using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class TentacleHomeSystem : ISystem
    {
        private Filter _filter;
        private Stash<TentacleComponent> _tentacleStash;
        private Stash<TentacleHomeFilter> _tentacleHomeStash;
        
        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<TentacleComponent>().With<TentacleHomeFilter>().Build();
            _tentacleStash = World.GetStash<TentacleComponent>();
            _tentacleHomeStash = World.GetStash<TentacleHomeFilter>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var tentacle = ref _tentacleStash.Get(entity);
                ref var tentacleHome = ref _tentacleHomeStash.Get(entity);
                
                Home(ref tentacle, ref tentacleHome);
            }
        }

        public void Home(ref TentacleComponent tentacle, ref TentacleHomeFilter tentacleHome)
        {
            // if (Vector3.Distance(tentacle.Root.position, tentacle.Home.position) > 0.1f)
            // {
            //     if (!tentacle.rope.distanceConstraintsEnabled)
            //     {
            //         tentacle.rope.distanceConstraintsEnabled = true;
            //     }
            //     
            //     float step = 20f * Time.deltaTime;
            //     tentacle.Root.position =
            //         Vector3.MoveTowards(tentacle.Root.position, tentacle.Home.position, step);
            //     
            //     
            // }

            // tentacle.Attach.enabled = false;
            if (tentacle.currentParticle <= -100)
            {
                tentacle.currentParticle = tentacle.rope.activeParticleCount - 1;
            }

            Debug.Log(tentacle.rope.restLength);
            if (tentacle.currentParticle > 0)
            {
                // if (tentacle.currentParticle <= 1)
                //     tentacle.rope.distanceConstraintsEnabled = true;
                    
                int solverIndex = tentacle.rope.solverIndices[tentacle.currentParticle];

                Vector3 target =
                    tentacle.rope.solver.transform.TransformPoint(
                        tentacle.rope.solver.positions[solverIndex]);

                tentacle.Root.position = Vector3.MoveTowards(
                    tentacle.Root.position,
                    target,
                    20f * Time.fixedDeltaTime);

                if (Vector3.Distance(tentacle.Root.position, target) < 0.02f)
                {
                    tentacle.currentParticle--;
                }
            }
            else
            {
                tentacle.currentParticle = -1000;
                tentacle.Provider.SetReady();
            }
        }
    }
}
