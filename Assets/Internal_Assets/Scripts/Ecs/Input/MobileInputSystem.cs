using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class MobileInputSystem : ISystem
    {
        private bool _isInit;

        private Vector2 _moveValue;

        private Filter _filter;
        private Stash<MobileInputComponent> _inputStash;

        public World World { get; set; }

        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<MobileInputComponent>().Build();
            this._inputStash = this.World.GetStash<MobileInputComponent>();
        }

        public void OnUpdate(float deltaTime)
        {
            if(InputInitManager.Instance == null) return;

            if (InputInitManager.Instance.MoveAction.inProgress)
            {
                _moveValue = InputInitManager.Instance.MoveAction.ReadValue<Vector2>();
            }
            else
            {
                _moveValue = Vector2.zero;
            }

            foreach (Entity entity in this._filter)
            {
                ref var inputComponent = ref _inputStash.Get(entity);
                CheckInput(ref inputComponent);
            }
        }
        
        private void CheckInput(ref MobileInputComponent mobileInput)
        {
            mobileInput.Rotation = _moveValue;
            mobileInput.Magnitude = _moveValue.magnitude;
        }
        
        private float Lerp01(float value, float maxValue) =>
            Mathf.Sign(value) * Mathf.Lerp(0, 1, Mathf.InverseLerp(0, maxValue, Mathf.Abs(value)));
    }
}
