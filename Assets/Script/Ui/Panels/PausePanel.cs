using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class PausePanel : MenuPanel
    {
        public void Button_Resume()
        {
            Main.ExitPanel();
        }

        public void Button_Settings()
        {
            Main.PushPanel<SettingsPanel>();
        }
        
        public void Button_MainMenu()
        {
            SceneManager.LoadScene("Scenes/MenuScene");
        }

        public void Button_Quit()
        {
            Application.Quit();
        }

        private void SetGamePaused(bool isPaused)
        {
            GameManager.Get.IsGamePaused = isPaused;
        }
    
        public override void OnFocused(bool isFocused)
        {
            if(isFocused)
            {
                SetGamePaused(true);
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }
    
        public override void OnRemoved()
        {
            SetGamePaused(false);
        }
    }
}