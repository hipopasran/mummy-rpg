using Scellecs.Morpeh;
using UnityEngine;

namespace Secret
{
    public class CameraScaleSystem : ISystem
    {
        private Filter _filter;
        private Stash<CameraScaleComponent> _cameraStash;
        private Stash<MobileInputComponent> _inputStash;

        public World World { get; set; }
        public void Dispose()
        {
            
        }

        public void OnAwake()
        {
            this._filter = this.World.Filter.With<CameraScaleComponent>().With<MobileInputComponent>().Build();
            this._inputStash = this.World.GetStash<MobileInputComponent>();
            this._cameraStash = this.World.GetStash<CameraScaleComponent>();
        }
        
        public void OnUpdate(float deltaTime)
        {
            foreach (var entity in _filter)
            {
                ref var input = ref _inputStash.Get(entity);
                ref var camera = ref _cameraStash.Get(entity);
                
                ScaleCamera(ref camera, ref input);
            }
        }

        private void ScaleCamera(ref CameraScaleComponent camera, ref  MobileInputComponent input)
        {
            camera.CinemachineCamera.Lens.FieldOfView =
                camera.MinFOV + (camera.MaxFOV - camera.MinFOV) * input.Magnitude;
        }
    }
}
