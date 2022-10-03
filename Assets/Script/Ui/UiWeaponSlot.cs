using System;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
using Util;
using Weapons;

namespace Ui
{
    public class UiWeaponSlot : MonoBehaviour
    {
        private const string UiWeaponPath = "Textures/Ui/Weapons/";

        public Image ImgWeapon;
        public Image ImgSelected;
        [NonSerialized] public WeaponTypes WeaponType;
        [NonSerialized] public TextMeshProUGUI LbSlotNumber;

        private void Awake()
        {
            LbSlotNumber = GetComponentInChildren<TextMeshProUGUI>();
        }

        public void SetSelected(bool selected)
        {
            ImgSelected.gameObject.SetActive(selected);
            LbSlotNumber.color = selected ? Color.cyan : Color.white;
        }

        public void SetSprite(Sprite sprite)
        {
            if(sprite == null)
            {
                Debug.Log("NULL sprite on set");
                return;
            }

            ImgWeapon.sprite = sprite;
        }

        public UiWeaponSlot SetSlot(WeaponTypes wepType, int slotNumber)
        {
            WeaponType = wepType;
            SetSprite(AssetHelper.LoadSprite(GetWeaponSpritePath(wepType)));
            LbSlotNumber.text = $"{slotNumber + 1}";
            return this;
        }

        private string GetSpriteName(WeaponTypes type)
        {
            return $"{type}".ToLowerInvariant();
        }

        private string GetWeaponSpritePath(WeaponTypes type)
        {
            return $"{UiWeaponPath}{GetSpriteName(type)}";
        }
    }
}