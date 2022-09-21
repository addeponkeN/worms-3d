using CameraSystem.CameraStates;
using PlayerControllers;
using UnityEngine;
using Weapons;

namespace GameStates
{
    public class GameStateActivePlayer : GameState
    {
        private Player _player;
        private PlayerWeaponManager _wepManager;

        private float _playTimer;
        private bool _timeOut;

        public GameStateActivePlayer(Player player)
        {
            _player = player;
            _wepManager = player.ControlManager.GetController<PlayerWeaponManager>();
        }

        public override void Init(GameStateManager manager)
        {
            base.Init(manager);

            _playTimer = 30f;
            _wepManager.WeaponFiredEvent += WepManagerOnWeaponFiredEvent;
            _player.ControlManager.ControllersEnabled = true;
            GameManager.Get.CamManager.SetMainState(new FollowPlayerState(_player));
        }

        private void WepManagerOnWeaponFiredEvent(BaseWeapon wep)
        {
            if(wep.IsProjectile)
            {
                var projectile = wep.GetProjectile();
                Manager.PushState(new GameStateSpectatePlayer(_player));
                Manager.PushState(new GameStateFollowObject(projectile));
                Exit();
            }
        }

        private void OnTimeOut()
        {
            Debug.Log("TIME OUT");
            Manager.PushState(new GameStateSpectatePlayer(_player));
            Exit();
        }

        public override void Update()
        {
            base.Update();

            GameManager.Get.Ui.AimCanvas.gameObject.SetActive(_wepManager.CurrentWeapon.IsAimDown);

            _playTimer -= Time.deltaTime;
            if(_playTimer <= 0)
            {
                if(!_timeOut)
                {
                    OnTimeOut();
                }
            }
        }

        public override void Exit()
        {
            base.Exit();
            _player.ControlManager.ControllersEnabled = false;
            GameManager.Get.Ui.AimCanvas.gameObject.SetActive(false);
        }
    }
}