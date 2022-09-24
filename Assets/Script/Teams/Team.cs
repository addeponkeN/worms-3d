using System.Collections.Generic;

namespace Teams
{
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

        public void RemovePlayer(Player player)
        {
            Players.Remove(player);
        }
    }
}