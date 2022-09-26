using Projectiles;

namespace Weapons
{
    public class WeaponGrenade : BaseWeapon
    {
        public override WeaponTypes WeaponType { get; } = WeaponTypes.Grenade;

        private BaseProjectile _projectile;

        public override void Init()
        {
            base.Init();
            IsProjectile = true;
            Power = 5000f;
        }
        
        public override BaseProjectile GetProjectile()
        {
            return _projectile;
        }

        public override void Fired()
        {
            base.Fired();
            _projectile = FireProjectile(ProjectileTypes.Grenade);
            Kill();
        }

    }
}