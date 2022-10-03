using System;
using PlayerControllers;
using UnityEngine;
using UnityEngine.UI;
using Weapons;

namespace Ui
{
    public class UiWeaponBar : MonoBehaviour
    {
        public int Spacing = 4;

        private UiWeaponSlot[] _slots;
        private int _weaponCount;
        private float _slotWidth;

        private void Start()
        {
            var allWeaponTypes = Enum.GetValues(typeof(WeaponTypes));
            _weaponCount = allWeaponTypes.Length;

            var prefItem = PrefabManager.GetPrefab("weapon_slot");
            var prefImage = prefItem.GetComponentInChildren<Image>();
            _slotWidth = prefImage.sprite.bounds.extents.x;

            var basePos = transform.position;

            _slots = new UiWeaponSlot[_weaponCount];

            foreach(WeaponTypes wepType in allWeaponTypes)
            {
                int i = (int)wepType;
                var weaponGo = Instantiate(prefItem, transform);
                weaponGo.name = $"Weapon_{wepType}";
                weaponGo.transform.position =
                    new Vector3(basePos.x + Spacing * i + _slotWidth * i, basePos.y, basePos.y);

                _slots[i] = weaponGo.GetComponent<UiWeaponSlot>().SetSlot(wepType, i);
            }

            SetWeaponBarInCenter();

            var controllerManager = GameManager.Get.PlayerManager.ControllerManager;
            var wepController = controllerManager.GetController<PlayerWeaponController>();
            wepController.WeaponChangedEvent += WeaponControllerOnWeaponChangedEvent;
        }
        
        private void WeaponControllerOnWeaponChangedEvent(BaseWeapon wep)
        {
            for(int i = 0; i < _slots.Length; i++)
                _slots[i].SetSelected(false);
            int selectedIndex = (int)wep.WeaponType;
            _slots[selectedIndex].SetSelected(true);
        }

        private void SetWeaponBarInCenter()
        {
            var rect = GetComponent<RectTransform>();
            float x = -(_slotWidth * _weaponCount + Spacing * _weaponCount) * .5f;
            float y = _slotWidth * .5f;
            rect.anchoredPosition = new Vector2(x, y);
        }
    }
}