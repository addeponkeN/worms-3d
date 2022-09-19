using Cinemachine;
using Projectiles;
using UnityEngine;

namespace CameraSystem.CameraStates
{
    public class FollowProjectileState : FollowState
    {
        private const float WaitTime = 2f;

        private BaseProjectile _proj;
        private float _timer;

        public FollowProjectileState(BaseProjectile proj) : base(proj.transform)
        {
            _proj = proj;
        }

        public override void Init()
        {
            base.Init();
            _timer = WaitTime;
        }

        public override void Update()
        {
            base.Update();

            if(!_proj.IsAlive)
            {
                _timer -= Time.deltaTime;

                if(_timer <= 0)
                {
                    IsAlive = false;
                }
            }
        }
    }
}