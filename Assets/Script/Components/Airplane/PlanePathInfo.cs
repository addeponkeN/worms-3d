using UnityEngine;

namespace Components
{
    public struct PlanePathInfo
    {
        public Vector3 StartPosition;
        public Vector3 Direction;

        public PlanePathInfo(Vector3 startPosition, Vector3 direction)
        {
            StartPosition = startPosition;
            Direction = direction;
        }
    }

}