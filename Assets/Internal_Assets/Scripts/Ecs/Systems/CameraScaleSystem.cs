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

                if (input.Magnitude > 0)
                {
                    if (!camera.UpOrDown)
                    {
                        camera.UpOrDown = true;
                        camera.Timer = 0f;
                    }
                    ScaleCamera(ref camera, ref input, deltaTime);
                }
                else
                {
                    if (camera.UpOrDown)
                    {
                        camera.UpOrDown = false;
                        camera.Timer = 0f;
                    }
                    if (camera.CinemachineCamera.Lens.FieldOfView > camera.MinFOV)
                    {
                        ScaleCameraToZero(ref camera, ref input, deltaTime);
                    }
                }
            }
        }

        private void ScaleCamera(ref CameraScaleComponent camera, ref  MobileInputComponent input, float deltaTime)
        {
            var fov = camera.MinFOV + (camera.MaxFOV - camera.MinFOV) * input.Magnitude;
            camera.Timer += deltaTime;
            // camera.CinemachineCamera.Lens.FieldOfView = fov;
            // camera.Timer = 0;
            // camera.UICamera.fieldOfView = camera.MinFOV + (camera.MaxFOV - camera.MinFOV) * input.Magnitude;

            camera.CinemachineCamera.Lens.FieldOfView = Mathf.Lerp(camera.CinemachineCamera.Lens.FieldOfView,
                fov, camera.Timer / camera.TimeToFOV);

            if (camera.CinemachineCamera.Lens.FieldOfView >= fov)
            {
                camera.Timer = 0f;
            }
        }

        private void ScaleCameraToZero(ref CameraScaleComponent camera, ref  MobileInputComponent input, float deltaTime)
        {
            camera.Timer += deltaTime;
            camera.CinemachineCamera.Lens.FieldOfView =
                Mathf.Lerp(camera.CinemachineCamera.Lens.FieldOfView, camera.MinFOV, camera.Timer/camera.TimeToZero);

            if (camera.CinemachineCamera.Lens.FieldOfView <= camera.MinFOV)
            {
                camera.Timer = 0f;
            }
        }
    }
}
