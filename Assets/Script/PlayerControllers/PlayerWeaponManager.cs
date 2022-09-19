using UnityEngine;
using Weapons;

namespace PlayerControllers
{
    public class PlayerWeaponManager : BasePlayerController
    {
        public BaseWeapon CurrentWeapon;

        private bool isWeaponActive;

        public override void Init()
        {
            base.Init();
            SetWeapon(new WeaponGrenade());
        }

        public void SetWeapon(BaseWeapon wep)
        {
            if(CurrentWeapon != null)
            {
                CurrentWeapon.Kill();
            }

            if(wep == null)
            {
                Debug.Log($"WEAPON: no weapon");
                CurrentWeapon = null;
                return;
            }

            CurrentWeapon = wep;
            CurrentWeapon.Manager = Manager;
            CurrentWeapon.Init();
            Debug.Log($"WEAPON: {wep.WeaponType.ToString()}");
        }

        public override void Update()
        {
            base.Update();

            if(CurrentWeapon != null)
            {
                CurrentWeapon.Update();
                if(CurrentWeapon.IsFired)
                {
                    SetWeapon(null);
                }
            }

            if(Input.GetKeyDown(KeyCode.Alpha0))
            {
                SetWeapon(new WeaponHand());
            }
            else if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                SetWeapon(new WeaponGrenade());
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                SetWeapon(new WeaponBazooka());
            }
        }
    }
}