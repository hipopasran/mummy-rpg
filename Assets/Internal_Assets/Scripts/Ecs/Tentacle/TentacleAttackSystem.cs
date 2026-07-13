using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class TentacleAttackSystem : ISystem
    {
        private Filter _filter;
        private Stash<TentacleComponent> _tentacleStash;
        private Stash<TentacleAttackFilter> _tentacleAttackStash;

        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<TentacleComponent>().With<TentacleAttackFilter>().Build();
            _tentacleStash = World.GetStash<TentacleComponent>();
            _tentacleAttackStash = World.GetStash<TentacleAttackFilter>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var tentacle = ref _tentacleStash.Get(entity);
                ref var tentaclAttack = ref _tentacleAttackStash.Get(entity);

                if (tentaclAttack.EnemyToAttack != null)
                {
                    Attack(ref tentacle, ref tentaclAttack);
                }
            }
        }

        public void Attack(ref TentacleComponent tentacle, ref TentacleAttackFilter tentacleAttack)
        {
            if (tentacleAttack.EnemyToAttack == null || !tentacleAttack.EnemyToAttack.gameObject.activeInHierarchy)
            {
                tentacle.Provider.SetHome();
                return;
            }
            tentacle.Root.LookAt(tentacleAttack.EnemyToAttack);

            if (Vector3.Distance(tentacle.Root.position, tentacleAttack.EnemyToAttack.position) < 0.01f)
            {
                tentacle.Root.position = tentacleAttack.EnemyToAttack.position;
            }
            else
            {
                float step = 30f * Time.deltaTime;
                tentacle.Root.position = Vector3.MoveTowards(tentacle.Root.position, tentacleAttack.EnemyToAttack.position, step);
            }
        }
    }
}
