using UnityEngine;

namespace GameStates
{
    /// <summary>
    /// default state
    /// the brain of all states
    /// the state decider
    /// </summary>
    public class GameStateMain : GameState
    {
        private float _thinkTimer = 0.1f;

        private void Think()
        {
            var playerManager = GameManager.Get.PlayerManager;
            var player = playerManager.GetNextActivePlayer();
            playerManager.SetActivePlayer(player);
            
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
            _thinkTimer -= Time.deltaTime;
            if(_thinkTimer <= 0f)
            {
                Think();
            }
        }
    }
}