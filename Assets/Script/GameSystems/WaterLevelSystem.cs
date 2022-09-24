using GameStates;
using VoxelEngine;

namespace GameSystems
{
    public class WaterLevelSystem : GameSystem
    {
        //  increase water level every 2 turns
        private int _turnInterval = 2;
        private int _counter;

        private float _increaseAmount = 1f;

        public override void OnNextPlayerTurn()
        {
            base.OnNextPlayerTurn();

            _counter++;
            IncreaseWaterLevel();
            if(_counter >= _turnInterval)
            {
            }
        }

        public void IncreaseWaterLevel()
        {
            GameStateManager.PushState(new GameStateIncreaseWaterLevel(_increaseAmount));
        }
    }
}