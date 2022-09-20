using GameStates;

namespace GameSystems
{
    public abstract class GameSystem
    {
        public GameStateManager GameStateManager { get; set; }
        
        public virtual void OnNextPlayerTurn()
        {
        }

        public virtual void OnGameStarted()
        {
        }
        
    }
}