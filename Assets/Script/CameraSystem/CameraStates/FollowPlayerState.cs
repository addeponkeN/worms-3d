namespace CameraSystem.CameraStates
{
    public class FollowPlayerState : FollowState
    {
        public FollowPlayerState(Player target) : base(target.CameraPosition)
        {
        }

        public override void Init()
        {
            base.Init();
        }

        public override void Update()
        {
            base.Update();
        }
    }
}