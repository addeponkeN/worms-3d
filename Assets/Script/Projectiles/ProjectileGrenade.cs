using UnityEngine;
using VoxelEngine;

namespace Projectiles
{
    public class ProjectileGrenade : BaseProjectile
    {
        public override ProjectileTypes Type { get; } = ProjectileTypes.Grenade;

        private const float FuseTime = 4f;
        private const int ExplodeRadius = 4;
        
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

        void Explode()
        {
            World.Get.SetVoxelCube(transform.position, ExplodeRadius, 0);
            Kill();
        }
    }
}