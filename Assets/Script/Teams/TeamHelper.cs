using UnityEngine;

namespace Teams
{
    public static class TeamHelper
    {
        public static readonly Color[] TeamColors = new[]
        {
            Color.green, Color.blue, Color.red, Color.yellow, Color.cyan, Color.magenta
        };

        public static readonly string[] TeamNames = new[]
        {
            "Green", "Blue", "Red", "Yellow", "Cyan", "Pink"
        };

        public static string GetTeamName(int i)
        {
            if(i >= TeamColors.Length)
                return "Unknown";
            return TeamNames[i];
        }
        
        public static Color GetTeamColor(int i)
        {
            if(i >= TeamColors.Length)
                return Color.black;
            return TeamColors[i];
        }
    }
}