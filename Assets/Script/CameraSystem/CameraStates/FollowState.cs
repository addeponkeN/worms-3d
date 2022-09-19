using UnityEngine;

namespace CameraSystem.CameraStates
{
    public class FollowState : ICameraState
    {
        public Camera Camera { get; set; }
        public bool IsAlive { get; set; }
        public CameraManager Manager { get; set; }

        protected readonly Transform Target;

        public FollowState(Transform target)
        {
            Target = target;
        }

        public virtual void Init()
        {
            Manager.MoveCam.Follow = Target;
            Manager.MoveCam.LookAt = Target;
            Manager.AimCam.Follow = Target;
            Manager.AimCam.LookAt = Target;
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnExit()
        {
            
        }
    }
}