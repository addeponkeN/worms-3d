using UnityEngine;

namespace PlayerControllers
{
    public abstract class BasePlayerController
    {
        public bool IsAlive { get; set; } = true;
        public PlayerControllerManager Manager { get; set; }
        public Player Player => Manager.Player;

        protected Transform Transform => Manager.PlayerGo.transform;

        public virtual void Init()
        {
        }

        public virtual void Update()
        {
        }

        public virtual void FixedUpdate()
        {
        }

        public virtual void OnEnabled(bool enabled)
        {
        }

        public virtual void Exit()
        {
        }
    }
}