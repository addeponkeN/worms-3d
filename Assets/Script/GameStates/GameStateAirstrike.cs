using CameraSystem.CameraStates;
using UnityEngine;
using Weapons;

namespace GameStates
{
    public class GameStateAirstrike : GameState
    {
        private float _timer;
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

            Debug.Log("airstrike");

            _plane = Object.Instantiate(PrefabManager.Get.GetPrefab("airplane")).GetComponent<Airplane>();
            _plane.SetStrikePosition(_wep.TargetPosition);
            _plane.BombsDroppedEvent += PlaneOnBombsDroppedEvent;

            GameManager.Get.CamManager.SetMainState(new FollowFollowable(_plane));
        }

        private void PlaneOnBombsDroppedEvent()
        {
            var corpse = new GameObject("corpse").AddComponent<Corpse>();
            corpse.transform.position = _plane.Transform.position;
            GameManager.Get.CamManager.SetMainState(new FollowFollowable(corpse, 2f));
            _bombsDropped = true;
        }
        
        public override void Update()
        {
            base.Update();
            if(_bombsDropped && (_timer -= Time.deltaTime) <= 0f)
            {
                Exit();
            }
        }
        
    }
}