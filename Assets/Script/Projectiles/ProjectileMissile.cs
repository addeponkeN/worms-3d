using System;
using UnityEngine;

namespace Projectiles
{
    public class ProjectileMissile : BaseProjectile, IExploder
    {
        public override ProjectileTypes Type { get; } = ProjectileTypes.Missile;

        //  IExploder
        public event Action<ExplodeData> ExplodeEvent;
        //  ---------

        public float ExplodeRadius = 6;
        public int Damage = 35;

        private Vector3 _baseRot;

        private bool _exploded;

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
            _exploded = true;
            ExplodeEvent?.Invoke(new ExplodeData(transform.position, ExplodeRadius, 35));
            Kill();
        }
    }
}