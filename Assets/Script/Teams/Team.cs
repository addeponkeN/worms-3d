using System.Collections.Generic;
using UnityEngine;
using Util;

namespace Teams
{
    public class Team
    {
        public bool IsDead;
        public List<Player> Players;

        private Indexer _index;
        private int _teamId;

        public Team(int teamId, int playerCount)
        {
            _teamId = teamId;
            Players = new List<Player>(playerCount);
        }

        public string GetTeamName() => TeamHelper.GetTeamName(_teamId);
        public Color GetColor() => TeamHelper.GetTeamColor(_teamId);
        public int GetTeamId() => _teamId;

        public Player GetNextPlayer()
        {
            return Players[_index.Next()];
        }

        public void AddPlayer(Player player)
        {
            player.AssignTeam(this);
            Players.Add(player);
            _index.Length = Players.Count;
            _index.SetCurrent(_index.Length);
        }

        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
            _index.Length = Players.Count;
            IsDead = Players.Count <= 0;
        }
    }
}