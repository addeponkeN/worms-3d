using UnityEngine;

namespace Teams
{
    public static class TeamHelper
    {
        public static readonly Color[] TeamColors = new[]
        {
            Color.green, Color.blue, Color.red, Color.yellow, Color.cyan, Color.magenta
        };

        public static Color GetTeamColor(int i)
        {
            if(i >= TeamColors.Length)
                return Color.black;
            return TeamColors[i];
        }
    }
}