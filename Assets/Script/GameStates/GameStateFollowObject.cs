using CameraSystem.CameraStates;
using UnityEngine;

namespace GameStates
{
    public interface IFollowable
    {
        public bool EndFollow { get; set; }
        public Transform transform { get; }
    }

    public class GameStateFollowObject : GameState
    {
        private IFollowable _followAble;
        private float _timer;

        public GameStateFollowObject(IFollowable followAble, float waitTimeAfterFollowEnded = 2f)
        {
            _followAble = followAble;
            _timer = waitTimeAfterFollowEnded;
        }

        public override void Init(GameStateManager manager)
        {
            base.Init(manager);
            GameManager.Get.CamManager.SetMainState(new FollowFollowable(_followAble));
        }

        public override void Update()
        {
            base.Update();
            if(_followAble == null || _followAble.EndFollow)
            {
                _timer -= Time.deltaTime;
                if(_timer <= 0)
                    Exit();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}