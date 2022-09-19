using System;
using UnityEngine;
using VoxelEngine;

namespace Projectiles
{
    public enum ProjectileTypes
    {
        None,
        Grenade,
        Missile
    }

    public struct ProjectileData
    {
        public float Force;
        public Vector3 Direction;

        public ProjectileData(float force, Vector3 direction)
        {
            Force = force;
            Direction = direction;
        }
    }

    [RequireComponent(typeof(Rigidbody))]
    public abstract class BaseProjectile : MonoBehaviour
    {
        public abstract ProjectileTypes Type { get; }

        public virtual bool IsAlive { get; set; } = true;

        protected float Force;
        protected Rigidbody Body;
        protected ProjectileData Data;

        private void Awake()
        {
            Body = GetComponent<Rigidbody>();
        }

        protected virtual void FixedUpdate()
        {
            if(transform.position.y < World.Get.WaterLevel)
            {
                Kill();
            }
        }

        public virtual void Init(ProjectileData data)
        {
            Data = data;
            Force = data.Force;

            Body.AddForce(Data.Direction * Data.Force);
        }

        public virtual void Kill()
        {
            IsAlive = false;
            Destroy(Body.gameObject);
        }
    }
}