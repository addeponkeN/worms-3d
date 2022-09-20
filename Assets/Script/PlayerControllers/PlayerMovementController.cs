using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControllers
{
    public class PlayerMovementController : BasePlayerController
    {
        private static readonly int AnimationParId = Animator.StringToHash("AnimationPar");

        public bool IsMoving;

        private Animator _anim;
        private Vector3 _moveDir;
        private float _turnVel;
        private InputAction _moveAction;

        public override void Init()
        {
            _anim = Player.Model.GetComponent<Animator>();
            _moveAction = Manager.Input.actions["Move"];
        }

        public override void Update()
        {
            var input = _moveAction.ReadValue<Vector2>();
            IsMoving = input != Vector2.zero;

            //  only allow input if grounded
            if(Player.CharController.isGrounded)
            {
                _moveDir = Vector3.zero;
                if(IsMoving)
                {
                    var dir = new Vector3(input.x, 0, input.y).normalized;

                    var playerTf = Manager.PlayerGo.transform;
                    var cam = Camera.main.transform;

                    var targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
                    var angle = Mathf.SmoothDampAngle(playerTf.eulerAngles.y, targetAngle, ref _turnVel, 0.1f);
                    playerTf.rotation = Quaternion.Euler(0f, angle, 0f);

                    _moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
                }
            }

            //  temp
            if(IsMoving)
            {
                _anim.SetInteger(AnimationParId, 1);
            }
            else
            {
                _anim.SetInteger(AnimationParId, 0);
            }

            Player.Body.Move(_moveDir.normalized * (Player.Stats.MoveSpeed * Time.deltaTime));
            // Player.CharController.Move(_moveDir.normalized * (Player.Stats.MoveSpeed * Time.deltaTime));
            //  CharacterController.isGrounded is always false if this isnt added
            // Player.CharController.Move(Vector3.down * 0.0000001f);
        }

        public override void FixedUpdate()
        {
        }
    }
}