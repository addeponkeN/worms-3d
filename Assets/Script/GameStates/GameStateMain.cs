using UnityEngine;

namespace GameStates
{
    /// <summary>
    /// the brain of all states
    /// the state decider
    /// </summary>
    public class GameStateMain : GameState
    {
        private float _thinkTimer = 0.1f;

        public override void Init(GameStateManager manager)
        {
            base.Init(manager);
        }

        private void Think()
        {
            var systems = GameManager.Get.Systems;
            for(int i = 0; i < systems.Count; i++)
            {
                systems[i].OnNextPlayerTurn();
            }

            var player = GameManager.Get.PlayerManager.SetNextActivePlayer();
            Manager.PushState(new GameStateActivePlayer(player));
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

        public override void Exit()
        {
            base.Exit();
            
        }
        
    }
}