using GameStates;

namespace GameSystems
{
    public class AirDropSystem : GameSystem
    {
        public override void OnGameStarted()
        {
            base.OnGameStarted();
            GameStateManager.PushState(new GameStateAirDrop());
        }

        public override void OnNextPlayerTurn()
        {
            base.OnNextPlayerTurn();
            GameStateManager.PushState(new GameStateAirDrop());
        }
        
    }
}