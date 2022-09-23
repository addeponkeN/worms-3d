using CameraSystem.CameraStates;
using UnityEngine;

namespace GameStates
{
    public interface IFollowable
    {
        public bool EndFollow { get; set; }
        public Transform Transform { get; }
    }

    public class GameStateFollowObject : GameState
    {
        private IFollowable _followAble;
        private float _timer;

        private bool _ended;

        public GameStateFollowObject(IFollowable followAble, float waitTimeAfterFollowEnded = 0f)
        {
            _followAble = followAble;
            _timer = waitTimeAfterFollowEnded;
        }

        public override void Init(GameStateManager manager)
        {
            base.Init(manager);
            GameManager.Get.CamManager.SetMainState(new FollowFollowable(_followAble));
        }

        private void SpectateCorpse()
        {
            var corpseGo = new GameObject();
            var corpse = corpseGo.AddComponent<Corpse>();
            corpseGo.transform.position = _followAble.Transform.position;
            GameManager.Get.CamManager.SetMainState(new FollowFollowable(corpse));
        }

        public override void Update()
        {
            base.Update();

            if(!_followAble.EndFollow)
                return;
            
            if(_timer > 0)
            {
                if(!_ended)
                {
                    _ended = true;
                    SpectateCorpse();
                }

                _timer -= Time.deltaTime;
            }
            else
            {
                Exit();
            }
        }

        public override void Exit()
        {
            base.Exit();
        }
    }
}