using System.Collections.Generic;
using GameStates;
using PlayerControllers;
using Teams;
using UnityEngine;
using Util;
using VoxelEngine;


public class PlayerManager : MonoBehaviour, ILoader
{
    public List<Team> Teams;
    public Player ActivePlayer;
    public PlayerControllerManager ControllerManager;

    private GameObject _playerContainerGo;
    private Indexer _teamIndex;

    private void Awake()
    {
        _playerContainerGo = new GameObject("PlayerContainer");
        Teams = new List<Team>();
        ControllerManager = gameObject.AddComponent<PlayerControllerManager>();
    }

    public void Load()
    {
        //  4 teams
        //  4 players per team
        var testData = new GameData()
        {
            Teams = new TeamData[]
            {
                new TeamData()
                {
                    PlayerCount = 4
                },
                new TeamData()
                {
                    PlayerCount = 4
                },
                new TeamData()
                {
                    PlayerCount = 4
                }
            }
        };

        CreateTeams(testData);
        ActivePlayer = Teams[0].Players[0];
    }

    public Team CurrentTeam() => Teams[_teamIndex];
    public Team NextTeam()
    {
        Team team;
        do
        {
            team = Teams[_teamIndex.Next()];
        } while(team.IsDead);

        return team;
    }

    public Player CurrentPlayer() => ActivePlayer;
    public Player NextPlayer()
    {
        var team = NextTeam();
        return team.GetNextPlayer();
    }

    public void SetActivePlayer(Player player)
    {
        ActivePlayer = player;
        ControllerManager.SetPlayer(player);
        Debug.Log($"Player: {player.GetPlayerId()},  team: {player.GetTeam().GetTeamName()}({player.GetTeam().GetTeamId()})");
    }

    private Player CreatePlayer(Vector3 position)
    {
        var player = Instantiate(PrefabManager.Get.GetPrefab("player"), position, Quaternion.identity,
            _playerContainerGo.transform).GetComponent<Player>();
        player.Life.DeathEvent += LifeOnDeathEvent;
        return player;
    }

    private void LifeOnDeathEvent(GameActor player)
    {
        player.Life.DeathEvent -= LifeOnDeathEvent;
        RemovePlayer(player as Player);
        player.Kill();
    }

    private void RemovePlayer(Player player)
    {
        var team = player.GetTeam();
        team.RemovePlayer(player);
        if(team.Players.Count <= 0)
        {
            Teams.Remove(team);
        }
    }

    private void CreateTeams(GameData gameData)
    {
        Teams.Clear();

        int teamId = 0;

        for(int i = 0; i < gameData.Teams.Length; i++)
        {
            var teamData = gameData.Teams[i];
            var team = new Team(teamId++, teamData.PlayerCount);

            for(int j = 0; j < teamData.PlayerCount; j++)
            {
                var position = World.Get.GetRandomSafePosition();
                var player = CreatePlayer(position);
                team.AddPlayer(player);
            }

            Teams.Add(team);
        }

        _teamIndex = Teams.Count;
        _teamIndex.SetCurrent(Teams.Count);
    }
    
}