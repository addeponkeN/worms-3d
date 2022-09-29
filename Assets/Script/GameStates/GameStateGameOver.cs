using Ui;
using UnityEngine;

namespace GameStates
{
    public class GameStateGameOver : GameState
    {
        public override void Init(GameStateManager manager)
        {
            base.Init(manager);
            
            Time.timeScale = 0f;

            var main = GameManager.Get.Ui.Main;
            main.CanExitCurrent = false;
            main.KeepBottomPanel = true;
            
            var panel = main.PushPanel<GameOverPanel>();
            panel.SetResults(GameManager.Get.PlayerManager.Teams);
            
            GameManager.Get.Ui.GamePanel.gameObject.SetActive(false);
        }

        public override void Exit()
        {
            base.Exit();
            var main = GameManager.Get.Ui.Main;
            main.CanExitCurrent = true;
            main.KeepBottomPanel = false;
            main.ExitPanel();
            Time.timeScale = 1f;
            GameManager.Get.Ui.GamePanel.gameObject.SetActive(true);
        }
    }
}