using UnityEngine;

namespace Util
{
    public struct Timer
    {
        private float _timer;

        public Timer(float time)
        {
            _timer = time;
        }

        /// <summary>
        /// </summary>
        /// <returns>timer is done (below 0)</returns>
        public bool Update()
        {
            return (_timer -= Time.deltaTime) <= 0;
        }

        public static implicit operator Timer(float time) => new Timer(time);
    }
}