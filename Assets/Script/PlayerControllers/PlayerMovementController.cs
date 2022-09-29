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

        private Vector3 CalculateMoveDirection()
        {
            var cam = GameManager.Get.CamManager.Cam.transform;
            var inputDirection = new Vector3(_latestInput.x, 0, _latestInput.y).normalized;
            var targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + cam.eulerAngles.y;

            return Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
        }

        private void RotateToDirection()
        {
            var tfPlayer = Manager.PlayerGo.transform;
            var tfCam = GameManager.Get.CamManager.Cam.transform;

            var inputDirection = new Vector3(_latestInput.x, 0, _latestInput.y).normalized;
            var targetAngle = Mathf.Atan2(inputDirection.x, inputDirection.z) * Mathf.Rad2Deg + tfCam.eulerAngles.y;
            var angle = Mathf.SmoothDampAngle(tfPlayer.eulerAngles.y, targetAngle, ref _turnVel, 0.1f);
            tfPlayer.rotation = Quaternion.Euler(0f, angle, 0f);
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
                    _moveDir = CalculateMoveDirection();
                }
            }

            if(_moveDir != Vector3.zero)
                RotateToDirection();

            Player.Body.Move(_moveDir.normalized * (Player.Stats.MoveSpeed * Time.deltaTime));
        }
    }
}