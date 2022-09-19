using Projectiles;

namespace Weapons
{
    public class WeaponBazooka : BaseWeapon
    {
        public override WeaponTypes WeaponType { get; } = WeaponTypes.Bazooka;

        public override void Init()
        {
            base.Init();
            Power = 3000f;
        }

        public override void Fired()
        {
            base.Fired();
            FireProjectile(ProjectileTypes.Missile);
        }
    }
}