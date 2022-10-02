using System;
using Components;
using Util;

namespace Projectiles
{
    public class ProjectileGrenade : BaseProjectile, IExploder
    {
        private const float FuseTime = 4f;
        
        public override ProjectileTypes Type { get; } = ProjectileTypes.Grenade;
        
        public event Action<ExplodeData> ExplodeEvent;
        public float ExplodeRadius = 4;
        public int Damage = 4;
        
        private Timer _fuseTimer;

        public override void Init(ProjectileData data)
        {
            base.Init(data);
            _fuseTimer = FuseTime;
        }

        protected override void Update()
        {
            base.Update();
            if(_fuseTimer.UpdateCheck())
            {
                Explode();
            }
        }

        private void Explode()
        {
            ExplodeEvent?.Invoke(new ExplodeData(transform.position, ExplodeRadius, Damage));
            Kill();
        }

    }
}