using GameStates;
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
    public abstract class BaseProjectile : MonoBehaviour, IFollowable
    {
        public abstract ProjectileTypes Type { get; }

        //  IFollowable
        public bool EndFollow { get; set; }
        public Transform Transform => transform;
        //  -----------

        public bool IsAlive { get; set; } = true;

        protected Rigidbody Body;
        protected ProjectileData Data;

        private void Awake()
        {
            Body = GetComponent<Rigidbody>();
        }

        protected virtual void FixedUpdate()
        {
            if(transform.position.y < World.Get.Water.WaterLevel)
            {
                Kill();
            }
        }

        public virtual void Init(ProjectileData data)
        {
            Data = data;
            Body.AddForce(Data.Direction * Data.Force);
        }

        public virtual void Kill()
        {
            IsAlive = false;
            EndFollow = true;
            Invoke(nameof(DestroySelf), 0.05f);
        }

        void DestroySelf()
        {
            Destroy(Body.gameObject);
        }
    }
}