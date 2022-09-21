using UnityEngine;

namespace Ui
{
    public class PausePanel : MonoBehaviour, IUiPanel
    {
        public MainCanvas Main { get; set; }

        public void Button_Resume()
        {
            Main.ExitPanel();
        }

        public void Button_Settings()
        {
            Main.PushPanel<SettingsPanel>();
        }

        public void Button_Quit()
        {
            Application.Quit();
        }

        private void SetGamePaused(bool isPaused)
        {
            GameManager.Get.IsGamePaused = isPaused;
        }
    
        public void OnFocused(bool isFocused)
        {
            if(isFocused)
                SetGamePaused(true);
        }
    
        public void OnRemoved()
        {
            SetGamePaused(false);
        }
    }
}