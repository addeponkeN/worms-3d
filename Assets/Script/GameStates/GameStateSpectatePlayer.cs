using CameraSystem.CameraStates;
using Components;
using UnityEngine;
using Util;

namespace GameStates
{
    public class GameStateSpectatePlayer : GameState
    {
        private Timer _timer;
        private Timer _forceExitTimer;
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
            _player.Life.DeathEvent += LifeOnDeathEvent;
            GameManager.Get.CamManager.SetMainState(new FollowPlayerState(_player));
        }

        private void LifeOnDeathEvent(GameActor player)
        {
            player.Life.DeathEvent -= LifeOnDeathEvent;
            var corpse = new GameObject("corpse").AddComponent<Corpse>();
            corpse.transform.position = player.transform.position;
            Manager.PushState(new GameStateFollowObject(corpse));
            Exit();
        }

        public override void Update()
        {
            base.Update();

            if(_timer.UpdateCheck() || _forceExitTimer.UpdateCheck())
            {
                Exit();
            }
        }
    }
}