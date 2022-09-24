using UnityEngine;
using VoxelEngine;

namespace Weapons
{
    public class WeaponHand : BaseWeapon
    {
        public override WeaponTypes WeaponType { get; } = WeaponTypes.Hand;

        public override void Init()
        {
            base.Init();
            IsProjectile = false;
        }

        public override void Update()
        {
            base.Update();

            if(Input.GetKeyDown(KeyCode.F))
            {
                var aimer = GameManager.Get.CamManager.Cam;
                var ray = new Ray(aimer.transform.position, aimer.transform.forward);

                if(Physics.Raycast(ray, out var info))
                {
                    World.Get.SetVoxelSphere(info.point, 5, 0);
                }
            }
            
        }
    }
}