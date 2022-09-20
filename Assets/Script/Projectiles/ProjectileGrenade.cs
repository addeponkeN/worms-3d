using System;
using UnityEngine;
using VoxelEngine;

namespace Projectiles
{
    public class ProjectileGrenade : BaseProjectile, IExploder
    {
        public override ProjectileTypes Type { get; } = ProjectileTypes.Grenade;

        private const float FuseTime = 4f;
        
        public event Action<ExplodeData> ExplodeEvent;
        public float ExplodeRadius = 4;
        public int Damage = 4;
        
        private float _fuseTimer;

        public override void Init(ProjectileData data)
        {
            base.Init(data);
            _fuseTimer = FuseTime;
        }

        private void Update()
        {
            _fuseTimer -= Time.deltaTime;
            if(_fuseTimer <= 0)
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