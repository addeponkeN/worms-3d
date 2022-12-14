using Components;
using UnityEngine;

namespace Weapons
{
    public class WeaponPistol : BaseWeapon
    {
        public override WeaponTypes WeaponType { get; } = WeaponTypes.Pistol;

        public int Damage = 7;
        
        private int _maxBulletCount = 3;
        private int _bullets;
        private int _magazineCount;

        public override void Init()
        {
            base.Init();
            IsProjectile = false;
            ChargeTime = 0f;

            _bullets = _maxBulletCount;
            _magazineCount = 1;
        }

        public override bool NeedsReloading()
        {
            return _bullets > 0 && _magazineCount <= 0;
        }

        public override void Update()
        {
            base.Update();
            IsReloaded = false;

            if(Input.GetKeyDown(KeyCode.R))
            {
                IsReloaded = true;
                Reload();
            }
        }

        public void Reload()
        {
            if(_bullets <= 0)
                return;

            _bullets--;
            _magazineCount = 1;
            Debug.Log($"reloaded, {_bullets} left");
        }

        public override void FireStart()
        {
            base.FireStart();

            CanBeSwapped = false;

            if(_magazineCount <= 0)
            {
                if(_bullets <= 0)
                {
                    Kill();
                }

                return;
            }

            var aimer = GameManager.Get.CamManager.Cam;
            var fw = aimer.transform.forward.normalized;
            var ray = new Ray(aimer.transform.position, fw);

            _magazineCount--;

            int layerMask = 1 << LayerMask.NameToLayer("Damageable");

            if(Physics.Raycast(ray, out var info, 9999f, layerMask))
            {
                var player = info.transform.GetComponentInParent<Player>();
                if(player != null)
                {
                    player.Life.TakeDamage(Damage);
                    player.Body.Push(ray.direction + Vector3.up * .4f, 1.5f);
                }
            }

            if(_bullets <= 0)
            {
                Kill();
            }
        }
    }
}