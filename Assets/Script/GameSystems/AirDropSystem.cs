using GameStates;
using UnityEngine;

namespace GameSystems
{
    public class AirDropSystem : GameSystem
    {
        public override void OnGameStarted()
        {
            base.OnGameStarted();
            GameStateManager.PushState(new GameStateAirDrop());

            var teams = GameManager.Get.PlayerManager.Teams;

            for(int i = 0; i < teams.Count; i++)
            {
                for(int j = 0; j < teams[i].Players.Count; j++)
                {
                    var p = teams[i].Players[j];
                    
                    var prefab = PrefabManager.Get.GetPrefab("airdrop");
                    var drop = Object.Instantiate(prefab).GetComponent<AirDrop>();
                    drop.transform.position = p.transform.position + new Vector3(2, 5, 2);
                    drop.ReleaseParachute();

                }
            }
        }

        public override void OnNextPlayerTurn()
        {
            base.OnNextPlayerTurn();
            GameStateManager.PushState(new GameStateAirDrop());
        }
        
    }
}