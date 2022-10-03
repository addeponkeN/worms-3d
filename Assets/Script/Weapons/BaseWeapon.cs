using AudioSystem;
using PlayerControllers;
using Projectiles;
using UnityEngine;
using Util;

namespace Weapons
{
    public enum WeaponTypes
    {
        Hand,
        Grenade,
        Bazooka,
        Pistol,
        Airstrike,
    }

    public abstract class BaseWeapon
    {
        public abstract WeaponTypes WeaponType { get; }

        public float GetWeaponChargeValue => _chargeTimer / ChargeTime;
        public float GetFinalPower => Power * GetWeaponChargeValue;

        public PlayerControllerManager Manager;
        public GameObject WeaponGo;
        public bool IsFired;
        public bool IsFireDown;
        public bool IsReloaded;
        public bool IsAimDown;
        public bool IsProjectile;
        public bool IsAlive = true;
        public bool CanBeSwapped = true;
        public bool AimeStanceChangedThisFrame;
        public float ChargeTime = 1.5f;

        public virtual bool NeedsReloading()
        {
            return false;
        }

        protected float Power = 5000f;

        private AudioSource _chargeAudio;
        private Vector3 _rotationOffset;
        private Transform _weaponOrigin;
        private bool _prevAimDown;
        private float _chargeTimer;
        
        public string GetWeaponName()
        {
            return WeaponType.ToString().ToLower();
        }

        public virtual void Init()
        {
            WeaponGo = Object.Instantiate(PrefabManager.Get.GetPrefab(WeaponType));

            _weaponOrigin = Manager.Player.WeaponOrigin.transform;
            _rotationOffset = WeaponGo.transform.eulerAngles;
        }

        public virtual void Update()
        {
            AimeStanceChangedThisFrame = false;

            var tf = WeaponGo.transform;
            tf.position = _weaponOrigin.transform.position;

            var eulerAngles = Manager.PlayerGo.transform.eulerAngles;
            var camEuler = GameManager.Get.CamManager.Cam.transform.eulerAngles;
            tf.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y - 90, _rotationOffset.z - camEuler.x);

            _prevAimDown = IsAimDown;
            IsAimDown = Input.GetMouseButton(1);

            if(IsAimDown != _prevAimDown)
            {
                AimeStanceChangedThisFrame = true;
                if(IsAimDown)
                {
                    var camManager = GameManager.Get.CamManager;
                    camManager.AimCam.Priority = 11;
                }
                else
                {
                    var camManager = GameManager.Get.CamManager;
                    camManager.AimCam.Priority = 9;
                }
            }

            if(IsAimDown)
            {
                if(IsFireDown)
                {
                    _chargeTimer += Time.deltaTime;
                    if(Input.GetMouseButtonUp(0) || _chargeTimer >= ChargeTime)
                        Fired();
                }
                else
                {
                    if(Input.GetMouseButtonDown(0))
                        FireStart();
                }
            }
        }

        protected BaseProjectile FireProjectile(ProjectileTypes type)
        {
            var aimer = GameManager.Get.CamManager.Cam;
            var projDirection = aimer.transform.forward + Vector3.up * 0.01f;
            projDirection.Normalize();

            var projectileGo = Object.Instantiate(PrefabManager.Get.GetPrefab(type));
            projectileGo.transform.position = WeaponGo.transform.position + projDirection * 2f;
            var projectile = projectileGo.GetComponent<BaseProjectile>();
            projectile.Init(new ProjectileData(GetFinalPower, projDirection));
            return projectile;
        }

        public virtual BaseProjectile GetProjectile()
        {
            return null;
        }

        public virtual void FireStart()
        {
            IsFireDown = true;
            _chargeTimer = 0f;
            _chargeAudio= AudioManager.PlaySfx("weaponcharging");
        }

        public virtual void OnEnabled(bool enabled)
        {
            if(!enabled)
                GameManager.Get.CamManager.AimCam.Priority = 9;
        }

        public virtual void Fired()
        {
            IsFired = true;
            IsFireDown = false;
            if(_chargeAudio.isPlaying)
                _chargeAudio.Stop();
            AudioManager.PlaySfx($"weapon_{GetWeaponName()}");
        }

        protected void ResetFire()
        {
            IsFired = false;
        }

        public virtual void Kill()
        {
            IsAlive = false;
            Object.Destroy(WeaponGo);
            var camManager = GameManager.Get.CamManager;
            camManager.AimCam.Priority = 9;
        }
    }
}