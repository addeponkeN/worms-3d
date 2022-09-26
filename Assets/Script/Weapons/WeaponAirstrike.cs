using UnityEngine;

namespace Weapons
{
    public class WeaponAirstrike : BaseWeapon
    {
        public override WeaponTypes WeaponType { get; } = WeaponTypes.Airstrike;

        public Vector3 TargetPosition;
        
        private Laser _laser;

        public override void Init()
        {
            base.Init();
            ChargeTime = 1f;

            _laser = Object.Instantiate(PrefabManager.Get.GetPrefab("laser")).GetComponent<Laser>();
            _laser.SetActivated(false);
        }

        public override void Fired()
        {
            base.Fired();
            Object.Destroy(_laser.gameObject);
            Kill();
        }
        
        public override void Update()
        {
            base.Update();

            if(IsAimDown)
            {
                var fw = GameManager.Get.CamManager.Cam.transform.forward;
                var ray = new Ray(WeaponGo.transform.position + fw * 2, fw);
                if(Physics.Raycast(ray, out var info))
                {
                    TargetPosition = info.point;
                    _laser.transform.position = WeaponGo.transform.position;
                    _laser.SetActivated(true);
                    _laser.SetTarget(TargetPosition);
                }
                else
                {
                    _laser.SetActivated(false);
                }
            }
            else
            {
                _laser.SetActivated(false);
            }
        }
    }
}