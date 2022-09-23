using Projectiles;

namespace Weapons
{
    public class WeaponBazooka : BaseWeapon
    {
        public override WeaponTypes WeaponType { get; } = WeaponTypes.Bazooka;

        private BaseProjectile _projectile;
        
        public override void Init()
        {
            base.Init();
            IsProjectile = true;
            Power = 3000f;
        }
        
        public override BaseProjectile GetProjectile()
        {
            return _projectile;
        }

        public override void Fired()
        {
            base.Fired();
           _projectile = FireProjectile(ProjectileTypes.Missile);
           Kill();
        }
    }
}