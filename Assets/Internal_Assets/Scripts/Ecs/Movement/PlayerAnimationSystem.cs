using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class PlayerAnimationSystem : ISystem
    {
        private Filter _filter;
        private Stash<PlayerAnimationComponent> _animationStash;
        private Stash<PlayerMovementComponent> _movementStash;
        private Stash<MobileInputComponent> _inputStash;

        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            _filter = World.Filter.With<PlayerAnimationComponent>().With<PlayerMovementComponent>(). With<MobileInputComponent>().Build();
            _animationStash = World.GetStash<PlayerAnimationComponent>();
            _movementStash = World.GetStash<PlayerMovementComponent>();
            this._inputStash = this.World.GetStash<MobileInputComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var animation = ref _animationStash.Get(entity);
                ref var movement = ref _movementStash.Get(entity);
                ref var input = ref _inputStash.Get(entity);
                
                Anim(ref animation, ref movement, ref input);
            }
        }

        private void Anim(ref PlayerAnimationComponent animation, ref PlayerMovementComponent movement, ref MobileInputComponent input)
        {
            if (input.Magnitude > 0f)
            {
                animation.Animator.SetBool("Walk", true); 
            }
            else
            {
                animation.Animator.SetBool("Walk", false); 
            }
        }
    }
}
