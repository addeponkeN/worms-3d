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
        /// <returns>if timer is done (equal or below 0)</returns>
        public bool CheckUpdate()
        {
            return (_timer -= Time.deltaTime) <= 0;
        }

        public static implicit operator Timer(float time) => new(time);
    }
}