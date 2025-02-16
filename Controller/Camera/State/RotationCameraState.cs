using System;
using UnityEngine;

namespace FireNBM
{
    public class RotationCameraState : ICameraState
    {
        public RotationCameraState(CameraControllerComp controller)
        {
        }

        public Enum FunGetTypeState() => TypeCameraState.Rotation;
        public void FunOnEnter() {}
        public void FunOnExit() {}
        
        public void FunHandle()
        {
        }
    }
} 