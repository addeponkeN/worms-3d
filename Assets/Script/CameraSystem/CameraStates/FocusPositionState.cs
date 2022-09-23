using UnityEngine;

namespace CameraSystem.CameraStates
{
    public class FocusPositionState : ICameraState
    {
        public Camera Camera { get; set; }
        public bool IsAlive { get; set; }
        public CameraManager Manager { get; set; }

        private Transform _target;

        public FocusPositionState(Transform target)
        {
            _target = target;
        }

        public void Init()
        {
            Manager.MoveCam.Follow = _target;
            Manager.MoveCam.LookAt = _target;
            Manager.AimCam.Follow = _target;
            Manager.AimCam.LookAt = _target;
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }

        public void OnExit()
        {
        }
    }
}