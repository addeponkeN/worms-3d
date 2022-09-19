using CameraSystem.CameraStates;
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
    }

    public abstract class BaseWeapon
    {
        private const float MaxCharge = 1.5f;
        
        public abstract WeaponTypes WeaponType { get; }
        
        public PlayerControllerManager Manager;
        public GameObject WeaponObject;
        public bool IsFired;
        public bool IsAimDown;

        protected bool IsFireDown;
        protected float Power = 5000f;
        protected float GetFinalPower => Power * (_chargeTimer / MaxCharge);

        private Vector3 _rotationOffset;
        private Transform _weaponOrigin;
        private bool _prevAimDown;
        private float _chargeTimer;

        public virtual void Init()
        {
            WeaponObject = Object.Instantiate(PrefabManager.Get.GetPrefab(WeaponType));

            _weaponOrigin = Manager.WeaponOrigin.transform;
            _rotationOffset = WeaponObject.transform.eulerAngles;
        }

        public virtual void Update()
        {
            var tf = WeaponObject.transform;
            tf.position = _weaponOrigin.transform.position;

            var eulerAngles = Manager.CurrentPlayerObj.transform.eulerAngles;
            var camEuler = GameManager.Get.CamManager.Camera.transform.eulerAngles;
            tf.eulerAngles = new Vector3(eulerAngles.x, eulerAngles.y - 90, _rotationOffset.z - camEuler.x);

            _prevAimDown = IsAimDown;
            IsAimDown = Input.GetMouseButton(1);

            if(IsAimDown != _prevAimDown)
            {
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
                    if(Input.GetMouseButtonUp(0) || _chargeTimer >= MaxCharge)
                        Fired();
                }
                else
                {
                    if(Input.GetMouseButtonDown(0))
                        FireStart();
                }
            }
        }

        protected void FireProjectile(ProjectileTypes type)
        {
            var aimer = GameManager.Get.CamManager.Camera;
            var dir = aimer.transform.forward + Vector3.up * 0.01f;
            dir.Normalize();
            
            var projectile = Object.Instantiate(PrefabManager.Get.GetPrefab(type));
            projectile.transform.position = WeaponObject.transform.position + dir * 0.75f;
            var pScript = projectile.GetComponent<BaseProjectile>();
            pScript.Init(new ProjectileData(GetFinalPower, dir));
            
            GameManager.Get.CamManager.SetMainState(new FollowProjectileState(pScript));
        }
        
        public virtual void FireStart()
        {
            IsFireDown = true;
        }
        
        public virtual void Fired()
        {
            IsFired = true;
            IsFireDown = false;
            Kill();
        }

        public virtual void Kill()
        {
            Object.Destroy(WeaponObject);
            var camManager = GameManager.Get.CamManager;
            camManager.AimCam.Priority = 9;
        }
        
    }
}