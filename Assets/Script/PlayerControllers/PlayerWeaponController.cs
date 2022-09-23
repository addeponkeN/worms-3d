using System;
using UnityEngine;
using Weapons;

namespace PlayerControllers
{
    public class PlayerWeaponController : BasePlayerController
    {
        public BaseWeapon CurrentWeapon;

        public event Action<BaseWeapon> WeaponFiredEvent;
        public event Action<BaseWeapon> WeaponChangedEvent;
        public event Action<BaseWeapon> WeaponDoneEvent;

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
            WeaponChangedEvent?.Invoke(CurrentWeapon);
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
                if(!CurrentWeapon.IsAlive)
                {
                    WeaponDoneEvent?.Invoke(CurrentWeapon);
                    UnequipWeapon();
                }

                if(CurrentWeapon.IsFired)
                {
                    WeaponFiredEvent?.Invoke(CurrentWeapon);
                }

                if(CurrentWeapon.CanBeSwapped)
                {
                    SelectWeaponInput();
                }
            }
            else
            {
                SelectWeaponInput();
            }
        }

        void SelectWeaponInput()
        {
            if(Input.GetKeyDown(KeyCode.Alpha1) || Input.GetKeyDown(KeyCode.Alpha0))
            {
                UnequipWeapon();
            }
            else if(Input.GetKeyDown(KeyCode.Alpha2))
            {
                EquipWeapon(new WeaponGrenade());
            }
            else if(Input.GetKeyDown(KeyCode.Alpha3))
            {
                EquipWeapon(new WeaponBazooka());
            }
            else if(Input.GetKeyDown(KeyCode.Alpha4))
            {
                EquipWeapon(new WeaponPistol());
            }
        }
    }
}