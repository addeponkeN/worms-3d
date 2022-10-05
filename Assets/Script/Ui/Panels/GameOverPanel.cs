using System.Collections.Generic;
using System.Linq;
using Teams;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Ui
{
    public class GameOverPanel : MenuPanel
    {
        [SerializeField] private GameObject _placingsContainer;

        public void SetResults(List<Team> teams)
        {
            string textWinner;

            if(teams.Count <= 0)
            {
                textWinner = "Draw!";
            }
            else
            {
                var winningTeam = teams.First();
                textWinner = $"Team {winningTeam.GetTeamName()} Won!";
            }

            var g = Instantiate(PrefabManager.GetPrefab("button"), Vector3.zero, Quaternion.identity,
                _placingsContainer.transform);
            g.transform.position = _placingsContainer.transform.position;
            var btText = g.GetComponentInChildren<TMP_Text>();
            btText.text = textWinner;
        }

        public override void OnFocused(bool isFocused)
        {
            if(isFocused)
            {
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
            }
        }

        public override void OnRemoved()
        {
        }

        public void Button_ReturnToMenu()
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene("Scenes/MenuScene");
        }
    }
}