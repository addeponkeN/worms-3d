using UnityEngine;

namespace PlayerControllers
{
    public abstract class BasePlayerController
    {
        public bool IsAlive { get; set; } = true;
        public PlayerControllerManager Manager { get; set; }

        protected Transform Transform => Manager.CurrentPlayerObj.transform;

        public virtual void Init()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void Exit()
        {
        }
    }
}