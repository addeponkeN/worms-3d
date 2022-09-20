namespace EntityComponents
{
    public abstract class BaseEntityComponent
    {
        public GameEntity Ent { get; set; }
        public bool IsAlive { get; set; } = true;
        
        public virtual void Init()
        {
            
        }
        public virtual void Update()
        {
        }
        
        public virtual void FixedUpdate()
        {
        }
        
    }
}