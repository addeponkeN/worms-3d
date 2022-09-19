using System;
using UnityEngine.InputSystem;

namespace PlayerControllers
{
    public class PlayerJumpController : BasePlayerController
    {
        private PlayerGravityController _gravityController;

        public event Action JumpedEvent;
        private InputAction _jumpAction;

        public override void Init()
        {
            _jumpAction = Manager.Input.FindAction("Jump");
            _gravityController = Manager.GetController<PlayerGravityController>();
        }

        public override void Update()
        {
            base.Update();
            if(_jumpAction.triggered)
            {
                Jump();
            }
        }

        private void Jump()
        {
            if(Manager.CharController.isGrounded)
                _gravityController.AddForce(Manager.Stats.JumpStrength);
            JumpedEvent?.Invoke();
        }
        
    }
}