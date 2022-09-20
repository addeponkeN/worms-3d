using System;
using UnityEngine;
using Weapons;

namespace PlayerControllers
{
    public class PlayerWeaponManager : BasePlayerController
    {
        public BaseWeapon CurrentWeapon;
        public event Action<BaseWeapon> WeaponFiredEvent;

        private bool _isWeaponActive;

        public override void Init()
        {
            base.Init();
            EquipWeapon(new WeaponGrenade());
        }

        public void UnequipWeapon()
        {
            EquipWeapon(new WeaponHand());
        }

        public void EquipWeapon(BaseWeapon wep)
        {
            if(CurrentWeapon != null)
            {
                CurrentWeapon.Kill();
            }

            CurrentWeapon = wep;
            CurrentWeapon.Manager = Manager;
            CurrentWeapon.Init();
        }

        public override void OnEnabled(bool enabled)
        {
            base.OnEnabled(enabled);
            CurrentWeapon?.OnEnabled(enabled);
        }

        public override void Update()
        {
            base.Update();

            if(CurrentWeapon != null)
            {
                CurrentWeapon.Update();
                if(CurrentWeapon.IsFired)
                {
                    WeaponFiredEvent?.Invoke(CurrentWeapon);
                    UnequipWeapon();
                }
            }

            if(Input.GetKeyDown(KeyCode.Alpha0))
            {
                UnequipWeapon();
            }
            else if(Input.GetKeyDown(KeyCode.Alpha1))
            {
                EquipWeapon(new WeaponGrenade());
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                EquipWeapon(new WeaponBazooka());
            }
        }
    }
}