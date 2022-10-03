using CameraSystem.CameraStates;
using Components;
using UnityEngine;
using Util;

namespace GameStates
{
    public class GameStateAirDrop : GameState
    {
        private Timer _lifeTimer;
        private AirDrop _drop;

        public override void Init(GameStateManager manager)
        {
            base.Init(manager);
            _lifeTimer = 2f;
            var prefab = PrefabManager.GetPrefab("airdrop");
            _drop = Object.Instantiate(prefab).GetComponent<AirDrop>();
            GameManager.Get.CamManager.SetMainState(new FollowFollowable(_drop));
        }

        public override void Update()
        {
            base.Update();

            if(!_drop.ReleasedParachute && Input.GetKeyDown(KeyCode.Space))
            {
                _drop.ReleaseParachute();
            }

            if(_drop.EndFollow && _lifeTimer.UpdateCheck())
            {
                Exit();
            }
        }
    }
}