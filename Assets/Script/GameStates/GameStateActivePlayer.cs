using CameraSystem.CameraStates;
using PlayerControllers;
using UnityEngine;
using Util;
using Weapons;

namespace GameStates
{
    public class Corpse : MonoBehaviour, IFollowable
    {
        public bool EndFollow { get; set; } = true;
        public Transform Transform => transform;

        private Timer _life = 3f;

        private void Update()
        {
            if(_life.Update())
            {
                Destroy(gameObject);
            }
        }
    }

    public class GameStateActivePlayer : GameState
    {
        public float PlayTimer => _playTimer;
        
        private PlayerWeaponController _wepManager;
        private Player _player;
        private float _playTimer;
        private bool _timeOut;

        public GameStateActivePlayer(Player player)
        {
            _player = player;
            _wepManager = GameManager.Get.PlayerManager.ControllerManager.GetController<PlayerWeaponController>();
        }

        public override void Init(GameStateManager manager)
        {
            base.Init(manager);

            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Locked;
            
            _playTimer = 30f;
            _wepManager.WeaponDoneEvent += WepManagerOnWeaponDoneEvent;
            _player.Life.DeathEvent += LifeOnDeathEvent;
            GameManager.Get.PlayerManager.ControllerManager.ControllersEnabled = true;
            GameManager.Get.CamManager.SetMainState(new FollowPlayerState(_player));
        }

        private void LifeOnDeathEvent(GameActor player)
        {
            OnDied();
        }

        private void WepManagerOnWeaponDoneEvent(BaseWeapon wep)
        {
            if(wep.IsProjectile)
            {
                Manager.PushState(new GameStateSpectatePlayer(_player));
                if(wep.IsFired)
                {
                    var projectile = wep.GetProjectile();
                    Manager.PushState(new GameStateFollowProjectile(projectile, 2f));
                }
                Exit();
            }
            else if(!wep.IsProjectile && _player.Life.IsAlive)
            {
                Manager.PushState(new GameStateSpectatePlayer(_player));
                Exit();
            }
        }

        private void OnTimeOut()
        {
            Manager.PushState(new GameStateSpectatePlayer(_player));
            Exit();
        }

        private void OnDied()
        {
            var corpseGo = new GameObject("corpse");
            var corpse = corpseGo.AddComponent<Corpse>();
            corpseGo.transform.position = _player.transform.position;
            Manager.PushState(new GameStateFollowObject(corpse));
            Exit();

            Debug.Log("player died - active");
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
            _wepManager.WeaponDoneEvent -= WepManagerOnWeaponDoneEvent;
            GameManager.Get.PlayerManager.ControllerManager.ControllersEnabled = false;
            GameManager.Get.Ui.AimCanvas.gameObject.SetActive(false);
        }
    }
}