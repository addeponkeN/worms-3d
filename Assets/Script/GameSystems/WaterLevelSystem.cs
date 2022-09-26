using GameStates;

namespace GameSystems
{
    public class WaterLevelSystem : GameSystem
    {
        //  increase water level every 4 turns
        private int _turnInterval = 4;
        private int _counter;

        private float _increaseAmount = 2f;

        public override void OnNextPlayerTurn()
        {
            base.OnNextPlayerTurn();

            _counter++;
            if(_counter >= _turnInterval)
            {
                IncreaseWaterLevel();
                _counter = 0;
            }
        }

        public void IncreaseWaterLevel()
        {
            GameStateManager.PushState(new GameStateIncreaseWaterLevel(_increaseAmount));
        }
    }
}