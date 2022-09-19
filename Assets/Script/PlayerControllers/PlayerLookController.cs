using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControllers
{
    public class PlayerLookController : BasePlayerController
    {
        public bool IsAiming;

        private InputAction _aimAction;

        public override void Init()
        {
            base.Init();
            _aimAction = Manager.Input.FindAction("Aim");
        }

        public override void Update()
        {
            base.Update();

            if(Input.GetMouseButton(1))
            {
                var cam = GameManager.Get.CamManager.Camera.transform;
                var rotation = Quaternion.Euler(0, cam.eulerAngles.y, 0);
                Transform.rotation = Quaternion.Lerp(Transform.rotation, rotation, 4f * Time.deltaTime);
            }
        }

        public void SetAimDown(bool isAiming)
        {
            IsAiming = isAiming;
            GameManager.Get.Ui.AimCanvas.enabled = IsAiming;
        }
    }
}