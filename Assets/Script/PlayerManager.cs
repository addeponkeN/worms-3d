using System;
using System.Collections.Generic;
using GameStates;
using PlayerControllers;
using UnityEngine;
using Util;
using VoxelEngine;

public class PlayerManager : MonoBehaviour, ILoader
{
    //  create a looping item get list
    //  for teams & players
    public List<Team> Teams;
    public Player ActivePlayer;
    public int CurrentTeamIndex => _index = (_index + 1) % Teams.Count;
    public event Action WeaponChangedEvent;

    public PlayerControllerManager ControllerManager;
    
    private int _index;
    
    public Team GetNextActiveTeam()
    {
        return Teams[CurrentTeamIndex];
    }

    private void Awake()
    {
        Teams = new List<Team>();
        ControllerManager = gameObject.AddComponent<PlayerControllerManager>();
    }

    public void Load()
    {
        var testData = new GameData()
        {
            Teams = new TeamData[]
            {
                new TeamData()
                {
                    PlayerCount = 5
                },
                new TeamData()
                {
                    PlayerCount = 5
                }
            }
        };

        CreateTeams(testData);
        SpawnPlayers();
        ActivePlayer = Teams[0].Players[0];
    }

    public Player GetNextActivePlayer()
    {
        var team = GetNextActiveTeam();
        return team.GetNextPlayer();
    }

    public void SetActivePlayer(Player player)
    {
        ActivePlayer = player;
        ControllerManager.SetPlayer(player);
    }

    private void CreateTeams(GameData gameData)
    {
        Teams.Clear();

        for(int i = 0; i < gameData.Teams.Length; i++)
        {
            var teamData = gameData.Teams[i];
            var team = new Team(teamData.PlayerCount);

            for(int j = 0; j < teamData.PlayerCount; j++)
            {
                var player = Instantiate(PrefabManager.Get.GetPrefab("player")).GetComponent<Player>();
                player.Init(team);
                team.Players.Add(player);
            }

            Teams.Add(team);
        }
    }

    private void SpawnPlayers()
    {
        var chunks = World.Get.GetChunkList();
        for(int i = 0; i < Teams.Count; i++)
        {
            var team = Teams[i];
            for(int j = 0; j < team.Players.Count; j++)
            {
                var p = team.Players[j];

                Vector3 finalPosition = Vector3.zero;
                int tries = 0;
                do
                {
                    tries++;
                    if(tries > 10)
                        break;
                    var randomChunk = chunks.Random();
                    finalPosition = randomChunk.ChunkGo.transform.position;
                    finalPosition.y = World.ChunkSize.y;
                } while(!IsSpawnPointValid(ref finalPosition));

                p.transform.position = finalPosition;

            }
        }

        // Teams[0].Players[0].transform.position = new Vector3(1, 40, 1);

    }

    private bool IsSpawnPointValid(ref Vector3 pos)
    {
        var worldLayer = LayerMask.NameToLayer("World");
        const float distanceToCheck = 500f;

        var ray = new Ray(pos, Vector3.down);

        if(Physics.Raycast(ray, out var info))
        {
            if(info.point.y > World.Get.WaterLevel)
            {
                pos = new Vector3(info.point.x, info.point.y + 1, info.point.z);
                return true;
            }

            return false;
        }

        return false;
    }

    private void Update()
    {
    }
    
}

public struct GameData
{
    public TeamData[] Teams;
}

public struct TeamData
{
    public int PlayerCount;
}

public class Team
{
    public int CurrentPlayerIndex => _index = (_index + 1) % Players.Count;
    
    public List<Player> Players;
    private int _index;

    public Team(int playerCount)
    {
        Players = new List<Player>(playerCount);
    }

    public Player GetNextPlayer()
    {
        return Players[CurrentPlayerIndex];
    }
}