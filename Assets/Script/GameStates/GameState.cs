namespace GameStates
{
    public abstract class GameState
    {
        protected GameStateManager Manager { get; private set; }
        public bool IsAlive { get; set; }
        
        public virtual void Init(GameStateManager manager)
        {
            IsAlive = true;
            Manager = manager;
        }

        public virtual void Update()
        {
        }
        
        public virtual void FixedUpdate()
        {
        }
        
        public virtual void Exit()
        {
            IsAlive = false;
        }
        
    }
}