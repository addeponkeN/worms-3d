using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControllers
{
    public class PlayerLookController : BasePlayerController
    {
        private const float PlayerRotationSpeed = 6f;
        
        private InputAction _aimAction;

        public override void Init()
        {
            base.Init();
            _aimAction = Manager.Input.actions["Aim"];
        }

        public override void Update()
        {
            base.Update();

            if(_aimAction.inProgress)
            {
                var cam = GameManager.Get.CamManager.Cam.transform;
                var rotation = Quaternion.Euler(0, cam.eulerAngles.y, 0);
                Transform.rotation =
                    Quaternion.Lerp(Transform.rotation, rotation, PlayerRotationSpeed * Time.deltaTime);
            }
        }
    }
}