using System.Collections.Generic;
using UnityEngine;

public class PlayerManager : MonoBehaviour
{
    public List<Team> Teams;

    private void Awake()
    {
        Teams = new List<Team>();
    }

    private void Start()
    {
    }

    private void Update()
    {
    }

    private void SpawnPlayers()
    {
    }
    
}

public class Team
{
    public List<Player> Players;

    public Team(int playerCount)
    {
        Players = new List<Player>();
    }
    
}