using CameraSystem.CameraStates;
using Components;
using UnityEngine;
using Util;
using Weapons;

namespace GameStates
{
    public class GameStateAirstrike : GameState
    {
        private Timer _timer;
        private bool _bombsDropped;
        private Airplane _plane;
        private WeaponAirstrike _wep;

        public GameStateAirstrike(WeaponAirstrike wep)
        {
            _wep = wep;
        }

        public override void Init(GameStateManager manager)
        {
            base.Init(manager);
            _timer = 6f;

            _plane = Object.Instantiate(PrefabManager.Get.GetPrefab("airplane")).GetComponent<Airplane>();
            _plane.SetStrikePosition(_wep.TargetPosition);
            _plane.BombsDroppedEvent += PlaneOnBombsDroppedEvent;

            GameManager.Get.CamManager.SetMainState(new FollowFollowable(_plane));
        }

        private void PlaneOnBombsDroppedEvent()
        {
            var corpse = new GameObject("corpse").AddComponent<Corpse>();
            corpse.transform.position = _plane.Transform.position;
            GameManager.Get.CamManager.SetMainState(new FollowFollowable(corpse, 2.5f));
            _bombsDropped = true;
        }
        
        public override void Update()
        {
            base.Update();
            if(_bombsDropped && _timer.UpdateCheck())
            {
                Exit();
            }
        }
        
    }
}