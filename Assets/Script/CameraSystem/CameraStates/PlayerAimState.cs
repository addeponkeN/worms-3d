using UnityEngine;

namespace CameraSystem.CameraStates
{
    public class PlayerAimState : FollowState
    {
        public PlayerAimState(Player target) : base(target.CameraAimPosition)
        {
        }

        public override void Init()
        {
            base.Init();
        }

        public override void OnExit()
        {
            base.OnExit();
        }
    }
}