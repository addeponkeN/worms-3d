using System;
using UnityEngine;

namespace PlayerControllers
{
    public class PlayerControllerManager : MonoBehaviour
    {
        public StatContainer Stats => CurrentPlayer.Stats;

        public GameObject CurrentPlayerObj;
        public Player CurrentPlayer;
        
        [NonSerialized] public GameObject Model;
        [NonSerialized] public GameObject WeaponOrigin;
        [NonSerialized] public CharacterController CharController;
        [NonSerialized] public bool Started = false;
        [NonSerialized] public bool ControllersEnabled = true;

        public InputCore Input;
        public ControllerSet ControlSet;

        public T GetController<T>() where T : BasePlayerController => ControlSet.GetController<T>();

        private void Awake()
        {
            ControlSet = new ControllerSet();
            ControlSet.Manager = this;
            Input = new InputCore();
            Input.Enable();
            SetActivePlayer(GameObject.Find("MainPlayer"));
        }

        private void Start()
        {
            AddController(new PlayerMovementController());
            AddController(new PlayerLookController());
            AddController(new PlayerJumpController());
            AddController(new PlayerGravityController());
            AddController(new PlayerWeaponManager());

            ControlSet.Init();

            Started = true;
        }

        public void SetActivePlayer(GameObject player)
        {
            CurrentPlayerObj = player;
            Model = CurrentPlayerObj.GetComponentInChildren<Animator>().gameObject;
            WeaponOrigin = GameObject.Find("WeaponOrigin");
            CharController = CurrentPlayerObj.GetComponent<CharacterController>();
        }

        public void AddController(BasePlayerController controller)
        {
            controller.Manager = this;
            ControlSet.AddController(controller);
        }

        private void Update()
        {
            if(ControllersEnabled)
                ControlSet.Update();
        }

        private void FixedUpdate()
        {
            if(ControllersEnabled)
                ControlSet.FixedUpdate();
        }

        private void LateUpdate()
        {
            ControlSet.LateUpdate();
        }
    }
}