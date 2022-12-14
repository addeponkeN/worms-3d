using AudioSystem;
using UnityEngine;
using UnityEngine.SceneManagement;
using VoxelEngine;

namespace Ui
{
    public class MainMenuPanel : MenuPanel
    {
        private void Start()
        {
            Main.KeepBottomPanel = true;
            World.Get.Load();
            AudioManager.PlayMusic("menu");
        }

        public void Button_Start()
        {
            Main.PushPanel<GameRulesPanel>();
        }

        public void Button_Settings()
        {
            Main.PushPanel<SettingsPanel>();
        }

        public void Button_Quit()
        {
            Application.Quit(0);
        }

        public override void OnFocused(bool isFocused)
        {
        }

        public override void OnRemoved()
        {
        }
    }
}
