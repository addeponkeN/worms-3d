using CameraSystem.CameraStates;
using Projectiles;
using UnityEngine;

namespace GameStates
{
    public class GameStateFollowProjectile : GameState
    {
        private BaseProjectile _p;
        private float _timer;

        private bool _ended;

        public GameStateFollowProjectile(BaseProjectile proj, float waitTimeAfterFollowEnded = 0f)
        {
            _p = proj;
            _timer = waitTimeAfterFollowEnded;
        }

        public override void Init(GameStateManager manager)
        {
            base.Init(manager);
            GameManager.Get.CamManager.SetMainState(new FollowFollowable(_p));
        }

        private void SpectateCorpse()
        {
            var corpseGo = new GameObject();
            var corpse = corpseGo.AddComponent<Corpse>();
            corpseGo.transform.position = _p.Transform.position;
            GameManager.Get.CamManager.SetMainState(new FollowFollowable(corpse));
        }

        public override void Update()
        {
            base.Update();

            if(!_p.EndFollow)
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
    }
}