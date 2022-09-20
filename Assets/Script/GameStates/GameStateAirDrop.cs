using CameraSystem.CameraStates;
using UnityEngine;

namespace GameStates
{
    public class GameStateAirDrop : GameState
    {
        private float _timer;
        private AirDrop _drop;

        public override void Init(GameStateManager manager)
        {
            base.Init(manager);
            Debug.Log("!! AIR DROP IS HAPPENING !!");
            _timer = 2f;

            var prefab = PrefabManager.Get.GetPrefab("airdrop");
            _drop = Object.Instantiate(prefab).GetComponent<AirDrop>();
            
            GameManager.Get.CamManager.SetMainState(new FollowFollowable(_drop));
        }

        public override void Update()
        {
            base.Update();

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
            Debug.Log("!! AIR DROP ENDED !!");
        }
    }
}