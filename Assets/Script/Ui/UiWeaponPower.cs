using GameStates;
using PlayerControllers;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace Ui
{
    public class UiWeaponPower : MonoBehaviour
    {
        public float Progress
        {
            get => _progress;
            set
            {
                _progress = value;
                _progressBar.value = _progress;
                _fill.color = Color.Lerp(Color.green, Color.red, Progress);
            }
        }
    
        [SerializeField] private Slider _progressBar;
        [SerializeField] private Image _fill;
        private float _progress;

        private void Start()
        {
            Progress = 0f;

            _progressBar.gameObject.SetActive(false);

            var controllerManager = GameManager.Get.PlayerManager.ControllerManager;
            var wepController = controllerManager.GetController<PlayerWeaponController>();
            wepController.WeaponChargingEvent += WepControllerOnWeaponChargingEvent;
            wepController.WeaponAimingEvent += WepControllerOnWeaponAimingEvent;
            GameManager.Get.StateManager.GameStateChangedEvent += StateManagerOnGameStateChangedEvent;
        }

        private void StateManagerOnGameStateChangedEvent(GameState obj)
        {
            Progress = 0f;
            _progressBar.gameObject.SetActive(false);
        
        }

        private void WepControllerOnWeaponAimingEvent(BaseWeapon weapon)
        {
            Progress = 0f;
            _progressBar.gameObject.SetActive(weapon.IsAimDown);
            Cursor.visible = false;
            Cursor.lockState = CursorLockMode.Confined;
        }

        private void WepControllerOnWeaponChargingEvent(BaseWeapon weapon)
        {
            if(weapon.ChargeTime > 0.1f)
            {
                Progress = weapon.GetWeaponChargeValue;
            }
            else
            {
                Progress = 0f;
            }
        }
    }
}