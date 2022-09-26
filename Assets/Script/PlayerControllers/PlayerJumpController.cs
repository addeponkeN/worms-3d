using System;
using EntityComponents;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControllers
{
    public class PlayerJumpController : BasePlayerController
    {
        public event Action JumpedEvent;

        private SimpleBody _body;
        private InputAction _jumpAction;
        private float _jumpCooldownTimer;

        public override void Init()
        {
            _jumpAction = Manager.Input.actions["Jump"];
            _body = Player.gameObject.GetComponent<SimpleBody>();
        }

        public override void Update()
        {
            base.Update();
            if(_jumpCooldownTimer > 0)
                _jumpCooldownTimer -= Time.deltaTime;
            else if(_jumpAction.triggered)
            {
                Jump();
            }
        }

        private void Jump()
        {
            _jumpCooldownTimer = 0.1f;
            if(Player.CharController.isGrounded)
                _body.Jump(Player.Stats.JumpStrength);
            JumpedEvent?.Invoke();
        }
    }
}