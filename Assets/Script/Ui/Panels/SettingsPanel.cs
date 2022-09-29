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

        public void Slider_MasterVolumeChanged()
        {
            GameSettings.MasterVolume.SetValue(_slMasterVolume.value);
            Debug.Log($"mastervol: {_slMasterVolume.value}");
        }

        public void Slider_SfxVolumeChanged()
        {
            GameSettings.SfxVolume.SetValue(_slSfxVolume.value);
        }

        public void Slider_MusicVolumeChanged()
        {
            GameSettings.MusicVolume.SetValue(_slMusicVolume.value);
        }

        public void Checkbox_Smile()
        {
            var text = _cbSmile.GetComponentInChildren<TMP_Text>();
            text.text = _cbSmile.isOn ? ":D" : ":(";
        }

        public void Button_Back()
        {
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