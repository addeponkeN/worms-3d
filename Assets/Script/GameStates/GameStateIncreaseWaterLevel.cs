using UnityEngine;
using VoxelEngine;

namespace GameStates
{
    public class GameStateIncreaseWaterLevel : GameState
    {
        private const float RaiseTime = 2f;
        private float _startValue;
        private float _newValue;
        private float _timer;

        public GameStateIncreaseWaterLevel(float increaseAmount)
        {
            var waterLevel = World.Get.Water.WaterLevel;
            _startValue = waterLevel;
            _newValue = waterLevel + increaseAmount;
        }

        public override void Update()
        {
            base.Update();

            _timer += Time.deltaTime;
            if(_timer >= RaiseTime)
            {
                _timer = RaiseTime;
                Exit();
            }

            var val = _timer / RaiseTime;
            World.Get.Water.WaterLevel = Mathf.Lerp(_startValue, _newValue, val);
        }
    }
}