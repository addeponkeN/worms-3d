using UnityEngine;

namespace EntityComponents
{
    public abstract class BaseEntityComponent : MonoBehaviour
    {
        public GameActor Ent { get; set; }
        public bool IsAlive { get; set; } = true;

        public virtual void Start()
        {
            Ent = gameObject.GetComponent<GameActor>();
        }

        public virtual void Update()
        {
            
        }
        
        public virtual void FixedUpdate()
        {
            
        }
        
    }
}