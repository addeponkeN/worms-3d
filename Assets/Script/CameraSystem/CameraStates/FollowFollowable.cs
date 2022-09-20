using GameStates;
using UnityEngine;

namespace CameraSystem.CameraStates
{
    public class FollowFollowable : FollowState
    {
        private IFollowable _followable;
        private float _timer;

        public FollowFollowable(IFollowable followable, float waitTimeAfterEndFollow = 2f) : base(followable.transform)
        {
            _followable = followable;
            _timer = waitTimeAfterEndFollow;
        }

        public override void Update()
        {
            base.Update();
            
            if(_followable == null || _followable.EndFollow)
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