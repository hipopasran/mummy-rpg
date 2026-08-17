using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class MovementSystem : ISystem
    {
        private Filter _filter;
        private Stash<MobileInputComponent> _inputStash;
        private Stash<PlayerMovementComponent> _playerMovementStash;

        public World World { get; set; }
        
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<MobileInputComponent>().With<PlayerMovementComponent>().Build();
            this._inputStash = this.World.GetStash<MobileInputComponent>();
            this._playerMovementStash = this.World.GetStash<PlayerMovementComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (Entity entity in this._filter)
            {
                ref var inputComponent = ref _inputStash.Get(entity);
                ref var moveComp = ref _playerMovementStash.Get(entity);

                if (InputInitManager.Instance.MoveAction.IsInProgress())
                {
                    Move(ref inputComponent, ref moveComp);
                }
                else
                {
                    StopMove(ref inputComponent, ref moveComp);
                }
            }
        }

        private void StopMove(ref MobileInputComponent input, ref PlayerMovementComponent move)
        {
            
        }

        private void Move(ref MobileInputComponent input, ref PlayerMovementComponent move)
        {
            move.Transform.forward =  Vector3.Lerp(move.Transform.forward, 
                new Vector3(input.Rotation.x, move.Transform.forward.y, input.Rotation.y),
                15f * Time.fixedDeltaTime);
            
            // TODO: Подключить скорость из прокачки игрока
            move.Agent.speed = move.WalkSpeed;

            // move
            move.Transform.position += move.Transform.forward * move.WalkSpeed * input.Rotation.magnitude * Time.deltaTime;
        }
    }
}
