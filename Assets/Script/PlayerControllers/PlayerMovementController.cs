using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControllers
{
    public class PlayerMovementController : BasePlayerController
    {
        public bool IsMoving;

        private InputAction _moveAction;
        private Vector3 _moveDir;
        private float _turnVel;

        private Vector3 _latestInput;

        private bool _movedWhileGrounded;

        public override void Init()
        {
            _moveAction = Manager.Input.actions["Move"];
        }

        Vector3 CalculateMoveDirection(Vector2 input)
        {
            var cam = Camera.main.transform;
            var dir = new Vector3(input.x, 0, input.y).normalized;
            var targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        void RotateToDirection()
        {
            var playerTf = Manager.PlayerGo.transform;
            var cam = Camera.main.transform;

            var dir = new Vector3(_latestInput.x, 0, _latestInput.y).normalized;
            var targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(playerTf.eulerAngles.y, targetAngle, ref _turnVel, 0.1f);
            playerTf.rotation = Quaternion.Euler(0f, angle, 0f);
        }

        public override void Update()
        {
            var input = _moveAction.ReadValue<Vector2>();
            if(input != Vector2.zero)
            {
                _latestInput = input;
                IsMoving = true;
            }
            else
            {
                IsMoving = false;
            }

            //  only allow input if grounded
            if(Player.CharController.isGrounded)
            {
                _moveDir = Vector3.zero;
                if(IsMoving)
                {
                    _moveDir = CalculateMoveDirection(input);
                }
            }

            if(_moveDir != Vector3.zero)
                RotateToDirection();

            Player.Body.Move(_moveDir.normalized * (Player.Stats.MoveSpeed * Time.deltaTime));
        }

        public override void FixedUpdate()
        {
        }
    }
}