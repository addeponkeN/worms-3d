using Components;

namespace CameraSystem.CameraStates
{
    public class FollowPlayerState : FollowState
    {
        public FollowPlayerState(Player target) : base(target.CameraPosition)
        {
        }
    }
}