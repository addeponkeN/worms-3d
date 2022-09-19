using CameraSystem.CameraStates;
using Projectiles;
using UnityEngine;
using Util;

namespace Weapons
{
    public class WeaponGrenade : BaseWeapon
    {
        public override WeaponTypes WeaponType { get; } = WeaponTypes.Grenade;

        public override void Init()
        {
            base.Init();
            Power = 5000f;
        }

        public override void Fired()
        {
            base.Fired();
            FireProjectile(ProjectileTypes.Grenade);
        }
    }
}