using UnityEngine;

namespace Components
{
    public struct ExplodeData
    {
        public Vector3 Position;
        public float Radius;
        public int Damage;

        public ExplodeData(Vector3 position, float radius, int damage)
        {
            Position = position;
            Radius = radius;
            Damage = damage;
        }
    }

}