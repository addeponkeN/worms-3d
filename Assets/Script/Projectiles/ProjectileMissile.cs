using UnityEngine;
using VoxelEngine;

namespace Projectiles
{
    public class ProjectileMissile : BaseProjectile
    {
        public override ProjectileTypes Type { get; } = ProjectileTypes.Missile;

        private const int ExplodeRadius = 6;
        private Vector3 _baseRot;

        public override void Init(ProjectileData data)
        {
            base.Init(data);
            _baseRot = transform.eulerAngles;
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.transform.gameObject.layer == LayerMask.NameToLayer("World"))
                Explode();
        }

        private void Update()
        {
            var tf = transform;
            tf.LookAt(tf.position + Body.velocity);
            var e = tf.eulerAngles;
            tf.eulerAngles = new Vector3(e.x + _baseRot.x, e.y + _baseRot.y, e.z + _baseRot.z);
        }

        void Explode()
        {
            World.Get.SetVoxelCube(transform.position, ExplodeRadius, 0);
            Kill();
        }
    }
}