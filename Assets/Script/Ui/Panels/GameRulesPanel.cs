using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

namespace Ui
{

    public class GameRulesInfo
    {
        public int TeamsCount;
        public int TeamSize;

        public bool EnableAirdrops;
        public bool EnableDangerZone;
    }
    
    public class GameRulesPanel : MenuPanel
    {
        [SerializeField] private Slider _slPlayers;
        [SerializeField] private Slider _slTeamSize;
        [SerializeField] private TMP_Text _lbPlayerCount;
        [SerializeField] private TMP_Text _lbTeamSize;
        [SerializeField] private Toggle _cbAirdrops;
        [SerializeField] private Toggle _cbDangerZones;

        private static GameRulesInfo GameRules => GameCore.Get.GameRules;

        public override void OnFocused(bool isFocused)
        {
        }

        public override void OnRemoved()
        {
        }

        private void Start()
        {
            Slider_Players();
            Slider_TeamSize();
            Checkbox_Airdrops();
            Checkbox_DangerZones();
        }
        
        public void Button_StartGame()
        {
            SceneManager.LoadScene("GameScene");
        }

        public void Button_Back()
        {
            Main.ExitPanel();
        }

        public int GetPlayerCount()
        {
            return GameRules.TeamsCount = (int)_slPlayers.value;
        }

        public int GetTeamSize()
        {
            return GameRules.TeamSize = (int)_slTeamSize.value;
        }

        public void Slider_Players()
        {
            _lbPlayerCount.text = GetPlayerCount().ToString();
        }

        public void Slider_TeamSize()
        {
            _lbTeamSize.text = GetTeamSize().ToString();
        }

        public void Checkbox_Airdrops()
        {
            GameRules.EnableAirdrops = _cbAirdrops.isOn;
        }

        public void Checkbox_DangerZones()
        {
            GameRules.EnableDangerZone = _cbDangerZones.isOn;
        }


    }
}