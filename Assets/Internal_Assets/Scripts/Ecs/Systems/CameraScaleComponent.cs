using Scellecs.Morpeh;
using Unity.Cinemachine;
using UnityEngine;
using Unity.IL2CPP.CompilerServices;

namespace Secret
{
    [System.Serializable]
    [Il2CppSetOption(Option.NullChecks, false)]
    [Il2CppSetOption(Option.ArrayBoundsChecks, false)]
    [Il2CppSetOption(Option.DivideByZeroChecks, false)]
    public struct CameraScaleComponent : IComponent
    {
        public CinemachineCamera CinemachineCamera;
        public Camera UICamera;
        public float MinFOV;
        public float MaxFOV;
        public float TimeToZero;
        public float Timer;
        public float TimeToFOV;
        public bool UpOrDown;
    }
}
