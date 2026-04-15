using Scellecs.Morpeh;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Secret
{
    public class MobileInputSystem : ISystem
    {
        private bool _isInit;
        
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
            foreach (Entity entity in this._filter)
            {
                ref var inputComponent = ref _inputStash.Get(entity);
                CheckInput(ref inputComponent);
            }
        }
        
        private void CheckInput(ref MobileInputComponent mobileInput)
        {

            
            // if (UnityEngine.Input.GetMouseButtonDown(0))
            // {
            //     if(EventSystem.current.IsPointerOverGameObject()) return;
            //     if (!_isInit)
            //     {
            //         _isInit = true;
            //         Debug.Log("Click");
            //         mobileInput.StartPos = Input.mousePosition;
            //         // mobileInput.startPos = new Vector2(Screen.width / 2, Screen.height / 2);
            //     }
            // }
            //
            // if (UnityEngine.Input.GetMouseButtonUp(0))
            // {
            //     mobileInput.StartPos = Vector3.zero;
            //     mobileInput.Direction = Vector3.zero;
            //     _isInit = false;
            // }
            //
            // if (UnityEngine.Input.GetMouseButton(0))
            // {
            //     if(EventSystem.current.IsPointerOverGameObject()) return;
            //     Debug.Log("Hold");
            //     var joystickDir = Input.mousePosition - mobileInput.StartPos;
            //     var magnitude = joystickDir.magnitude;
            //     if (magnitude > 1f)
            //     {
            //         var position = joystickDir.normalized * Mathf.Clamp(magnitude, 0, 50);
            //         var dir = new Vector2(Lerp01(position.x, 50f),
            //             Lerp01(position.y, 50f));
            //
            //         // var position = joystickDir.normalized;
            //         mobileInput.Direction = new Vector3(dir.x,0,dir.y);
            //     }
            // }
        }
        
        private float Lerp01(float value, float maxValue) =>
            Mathf.Sign(value) * Mathf.Lerp(0, 1, Mathf.InverseLerp(0, maxValue, Mathf.Abs(value)));
    }
}
