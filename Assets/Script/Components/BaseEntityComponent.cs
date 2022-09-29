using UnityEngine;

namespace Components
{
    public abstract class BaseEntityComponent : MonoBehaviour
    {
        public GameActor Actor { get; set; }

        protected virtual void Start()
        {
            Actor = gameObject.GetComponent<GameActor>();
        }

        protected virtual void Update()
        {
        }
        
        protected virtual void FixedUpdate()
        {
        }
        
    }
}