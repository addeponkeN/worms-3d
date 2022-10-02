using UnityEngine;
using UnityEngine.UI;

namespace Ui
{
    public class GameRulesPanel : MenuPanel
    {
        [SerializeField] private Slider _slPlayers;
        [SerializeField] private Slider _slTeamSize;
        
        public override void OnFocused(bool isFocused)
        {
        }

        public override void OnRemoved()
        {
        }
    }
}