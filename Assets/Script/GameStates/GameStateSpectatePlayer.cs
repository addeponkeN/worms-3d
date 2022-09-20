using CameraSystem.CameraStates;
using UnityEngine;

namespace GameStates
{
    public class GameStateSpectatePlayer : GameState
    {
        private float _timer;
        private float _forceExitTimer;
        private Player _player;

        public GameStateSpectatePlayer(Player player, float time = 3f)
        {
            _timer = time;
            _player = player;
            _forceExitTimer = 20f;
        }

        public override void Init(GameStateManager manager)
        {
            base.Init(manager);

            GameManager.Get.CamManager.SetMainState(new FollowPlayerState(_player));
        }

        public override void Update()
        {
            base.Update();

            _timer -= Time.deltaTime;
            _forceExitTimer -= Time.deltaTime;

            if(!_player.CharController.isGrounded)
            {
                _timer = 2f;
            }

            if(_timer <= 0 || _forceExitTimer <= 0)
            {
                Exit();
            }
        }
    }
}