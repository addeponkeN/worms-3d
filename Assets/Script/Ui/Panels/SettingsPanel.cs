using System;
using Settings;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class SettingsPanel : MenuPanel
    {
        [SerializeField] private Slider _slMasterVolume;
        [SerializeField] private Slider _slSfxVolume;
        [SerializeField] private Slider _slMusicVolume;
        [SerializeField] private Toggle _cbSmile;

        private void Start()
        {
            _slMasterVolume.value = GameSettings.Get.MasterVolume.Value;
            _slSfxVolume.value = GameSettings.Get.SfxVolume.Value;
            _slMusicVolume.value = GameSettings.Get.MusicVolume.Value;
        }

        public void Slider_MasterVolumeChanged()
        {
            GameSettings.Get.MasterVolume.SetValue(_slMasterVolume.value);
        }

        public void Slider_SfxVolumeChanged()
        {
            GameSettings.Get.SfxVolume.SetValue(_slSfxVolume.value);
        }

        public void Slider_MusicVolumeChanged()
        {
            GameSettings.Get.MusicVolume.SetValue(_slMusicVolume.value);
        }

        public void Checkbox_Smile()
        {
            var text = _cbSmile.GetComponentInChildren<TMP_Text>();
            text.text = _cbSmile.isOn ? ":D" : ":(";
        }

        public void Button_Back()
        {
            GameSettings.Save();
            Main.ExitPanel();
        }

        public override void OnFocused(bool isFocused)
        {
        }

        public override void OnRemoved()
        {
        }
    }
}