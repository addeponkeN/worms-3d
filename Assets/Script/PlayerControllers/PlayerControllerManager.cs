using System;
using GameStates;
using UnityEngine;
using UnityEngine.InputSystem;

namespace PlayerControllers
{
    public class PlayerControllerManager : MonoBehaviour, ILoader
    {
        [NonSerialized] public GameObject PlayerGo;
        [NonSerialized] public Player Player;
        [NonSerialized] public bool Started = false;

        public PlayerInput Input => GameManager.Get.PlayerInput;
        public ControllerSet ControlSet;

        private bool _controllersEnabled = false;
        public bool ControllersEnabled
        {
            get => _controllersEnabled;
            set
            {
                _controllersEnabled = value;
                Input.enabled = _controllersEnabled;
                for(int i = 0; i < ControlSet.Controllers.Count; i++)
                    ControlSet.Controllers[i].OnEnabled(_controllersEnabled);
            }
        }
        
        public T GetController<T>() where T : BasePlayerController => ControlSet.GetController<T>();

        private void Awake()
        {
            ControlSet = new ControllerSet();
            ControlSet.Manager = this;

            PlayerGo = gameObject;
            Player = gameObject.GetComponent<Player>();
            
            AddController(new PlayerMovementController());
            AddController(new PlayerLookController());
            AddController(new PlayerJumpController());
            AddController(new PlayerWeaponController());
        }

        public void SetPlayer(Player player)
        {
            PlayerGo = player.gameObject;
            Player = player;
            ControlSet.Init();
        }

        private void Start()
        {
            Started = true;
        }

        public void Load()
        {
        }

        public void AddController(BasePlayerController controller)
        {
            controller.Manager = this;
            ControlSet.AddController(controller);
        }

        private void Update()
        {
            if(ControllersEnabled && Player.Life.IsAlive)
                ControlSet.Update();
        }

        private void FixedUpdate()
        {
            if(ControllersEnabled && Player.Life.IsAlive)
                ControlSet.FixedUpdate();
        }

        private void LateUpdate()
        {
            ControlSet.LateUpdate();
        }

    }
}