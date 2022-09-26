using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class SettingsPanel : MenuPanel
    {
        [SerializeField] private Slider _slMasterVolume;

        public void Slider_MasterVolumeChanged()
        {
            GameSettings.MasterVolume = _slMasterVolume.value;
        }
        
        public void Slider_MusicVolumeChanged()
        {
            GameSettings.MasterVolume = _slMasterVolume.value;
        }

        public void Button_Apply()
        {
            Main.ExitPanel();
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