using Components;
using GameStates;
using UnityEngine;

namespace GameSystems
{
    public class AirDropSystem : GameSystem
    {
        private int _roundInterval = 3;
        private int _counter = -1;

        public override void OnGameStarted()
        {
            base.OnGameStarted();
            GameStateManager.PushState(new GameStateAirDrop());
        }

        public override void OnNextPlayerTurn()
        {
            base.OnNextPlayerTurn();

            _counter++;

            if(_counter >= _roundInterval)
            {
                GameStateManager.PushState(new GameStateAirDrop());
                _counter = 0;
            }
        }

        private void OmegaSpawnAirDrops()
        {
            //  spawns many airdrops near all players
            var teams = GameManager.Get.PlayerManager.Teams;
            for(int i = 0; i < teams.Count; i++)
            {
                for(int j = 0; j < teams[i].Players.Count; j++)
                {
                    var p = teams[i].Players[j];

                    var prefab = PrefabManager.GetPrefab("airdrop");
                    var drop = Object.Instantiate(prefab).GetComponent<AirDrop>();
                    drop.transform.position = p.transform.position + new Vector3(2, 5, 2);
                    drop.ReleaseParachute();
                }
            }
        }
    }
}