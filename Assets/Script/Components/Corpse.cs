using GameStates;
using UnityEngine;
using Util;

namespace Components
{
    public class Corpse : MonoBehaviour, IFollowable
    {
        public bool EndFollow { get; set; } = true;
        public Transform Transform => transform;

        private Timer _life = 4f;
    
        private void Update()
        {
            if(_life.UpdateCheck())
            {
                Destroy(gameObject);
            }
        }
    }
}