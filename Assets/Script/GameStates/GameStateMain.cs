using Util;

namespace GameStates
{
    public class GameStateMain : GameState
    {
        private Timer _thinkTimer = 0.1f;

        private void DecideState()
        {
            var pm = GameManager.Get.PlayerManager;

            if(pm.Teams.Count <= 1)
            {
                Manager.PushState(new GameStateGameOver());
                Exit();
                return;
            }

            var player = pm.NextPlayer();
            pm.SetActivePlayer(player);

            Manager.PushState(new GameStateActivePlayer(player));

            var systems = GameManager.Get.Systems;
            for(int i = 0; i < systems.Count; i++)
            {
                systems[i].OnNextPlayerTurn();
            }

            Exit();
        }

        public override void Update()
        {
            base.Update();
            if(_thinkTimer.UpdateCheck())
            {
                DecideState();
            }
        }
    }
}