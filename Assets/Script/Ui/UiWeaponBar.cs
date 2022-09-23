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

        private int _weaponCount;
        private float _slotDimensions;

        private UiWeaponSlot[] _slots;

        private void Start()
        {
            var weaponTypes = Enum.GetValues(typeof(WeaponTypes));
            _weaponCount = weaponTypes.Length;

            var prefItem = PrefabManager.Get.GetPrefab("weapon_slot");

            var prefImage = prefItem.GetComponentInChildren<Image>();
            _slotDimensions = prefImage.sprite.bounds.extents.x;

            var basePos = transform.position;

            _slots = new UiWeaponSlot[_weaponCount];

            foreach(WeaponTypes wepType in weaponTypes)
            {
                int i = (int)wepType;
                var weaponGo = Instantiate(prefItem, transform);
                weaponGo.name = $"Weapon_{wepType}";
                weaponGo.transform.position =
                    new Vector3(basePos.x + Spacing * i + _slotDimensions * i, basePos.y, basePos.y);

                _slots[i] = weaponGo.GetComponent<UiWeaponSlot>().SetSlot(wepType, i);
            }

            SetBarInCenter();

            var controllerManager = GameManager.Get.PlayerManager.ControllerManager;
            var wepManager = controllerManager.GetController<PlayerWeaponController>();
            wepManager.WeaponChangedEvent += WepManagerOnWeaponChangedEvent;
        }

        private void WepManagerOnWeaponChangedEvent(BaseWeapon wep)
        {
            for(int i = 0; i < _slots.Length; i++)
                _slots[i].SetSelected(false);
            int selectedIndex = (int)wep.WeaponType;
            _slots[selectedIndex].SetSelected(true);
        }

        private void SetBarInCenter()
        {
            var rect = GetComponent<RectTransform>();
            float x = -(_slotDimensions * _weaponCount + Spacing * _weaponCount) * .5f;
            float y = _slotDimensions * .5f;
            rect.anchoredPosition = new Vector2(x, y);
        }
    }
}