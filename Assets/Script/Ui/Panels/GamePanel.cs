using GameStates;
using PlayerControllers;
using TMPro;
using UnityEngine;
using Weapons;

namespace Ui
{
    public class GamePanel : MenuPanel
    {
        [SerializeField] private TMP_Text lbPlayTimer;
        [SerializeField] private TMP_Text lbReload;

        private PlayerWeaponController _wepController;

        private void Awake()
        {
            lbReload.gameObject.SetActive(false);
        }

        private void Start()
        {
            _wepController = GameManager.Get.PlayerManager.ControllerManager.GetController<PlayerWeaponController>();
            _wepController.WeaponFiredEvent += WepControllerOnWeaponFiredEvent;
            _wepController.WeaponReloadedEvent += WepControllerOnWeaponReloadedEvent;
            _wepController.WeaponDoneEvent += WepControllerOnWeaponDoneEvent;
        }

        private void WepControllerOnWeaponDoneEvent(BaseWeapon wep)
        {
            lbReload.gameObject.SetActive(false);
        }

        private void WepControllerOnWeaponReloadedEvent(BaseWeapon wep)
        {
            lbReload.gameObject.SetActive(false);
        }

        private void WepControllerOnWeaponFiredEvent(BaseWeapon wep)
        {
            if (wep.NeedsReloading())
            {
                lbReload.gameObject.SetActive(true);
                lbReload.text = "Reload Weapon (R)";
            }
        }

        private void Update()
        {
            var state = GameManager.Get.GetGameState();
            if (state is GameStateActivePlayer player)
            {
                lbPlayTimer.text = $"{(int)player.PlayTimer}";
            }
        }

        public override void OnFocused(bool isFocused)
        {
        }

        public override void OnRemoved()
        {
            lbReload.gameObject.SetActive(false);
        }
    }
}