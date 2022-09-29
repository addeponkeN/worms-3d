using UnityEngine;

namespace Util
{
    public struct Timer
    {
        public static implicit operator Timer(float time) => new(time);
        
        private float _timer;

        public Timer(float time)
        {
            _timer = time;
        }

        /// <summary>
        /// </summary>
        /// <returns>if timer is done (equal or below 0)</returns>
        public bool UpdateCheck()
        {
            return (_timer -= Time.deltaTime) <= 0;
        }

    }
}