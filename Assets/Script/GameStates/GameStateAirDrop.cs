using CameraSystem.CameraStates;
using UnityEngine;
using UnityEngine.InputSystem;

namespace GameStates
{
    public class GameStateAirDrop : GameState
    {
        private float _timer;
        private AirDrop _drop;

        private InputAction skipAction;

        public override void Init(GameStateManager manager)
        {
            base.Init(manager);
            // Debug.Log("!! AIR DROP IS HAPPENING !!");
            _timer = 2f;

            var prefab = PrefabManager.Get.GetPrefab("airdrop");
            _drop = Object.Instantiate(prefab).GetComponent<AirDrop>();

            GameManager.Get.CamManager.SetMainState(new FollowFollowable(_drop));

            skipAction = GameManager.Get.PlayerInput.actions["Jump"];
        }

        public override void Update()
        {
            base.Update();

            //                              this new input system rarely works
            // if(!_drop.ReleasedParachute && skipAction.triggered)
            if(!_drop.ReleasedParachute && Input.GetKeyDown(KeyCode.Space))
            {
                _drop.ReleaseParachute();
            }

            if(_drop.EndFollow)
            {
                _timer -= Time.deltaTime;
                if(_timer <= 0)
                {
                    Exit();
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            // Debug.Log("!! AIR DROP ENDED !!");
        }
    }
}