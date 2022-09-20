namespace Weapons
{
    public class WeaponHand : BaseWeapon
    {
        public override WeaponTypes WeaponType { get; } = WeaponTypes.Hand;

        public override void Init()
        {
            base.Init();
            IsProjectile = false;
        }
    }
}