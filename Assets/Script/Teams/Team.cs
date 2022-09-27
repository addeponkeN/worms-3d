using System.Collections.Generic;
using UnityEngine;

namespace Teams
{
    public class Team
    {
        public int CurrentPlayerIndex => _index = (_index + 1) % Players.Count;

        public bool IsDead;
        public List<Player> Players;

        private int _index;
        private int _teamId;

        public Team(int teamId, int playerCount)
        {
            _teamId = teamId;
            Players = new List<Player>(playerCount);
        }

        public Color GetColor() => TeamHelper.GetTeamColor(_teamId);
        public int GetTeamId() => _teamId;

        public Player GetNextPlayer()
        {
            return Players[CurrentPlayerIndex];
        }

        public void AddPlayer(Player player)
        {
            player.AssignTeam(this);
            Players.Add(player);
        }

        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
            IsDead = Players.Count <= 0;
        }
    }
}