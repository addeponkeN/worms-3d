using System.Collections.Generic;
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
            var offset = 150f;
            for(int i = 0; i < teams.Count; i++)
            {
                var t = teams[i];
                var pos = new Vector2(0f, offset * i);
                var g = Instantiate(PrefabManager.Get.GetPrefab("button"), Vector3.zero, Quaternion.identity,
                    _placingsContainer.transform);

                var tfRect = g.GetComponent<RectTransform>();
                tfRect.localPosition = new Vector3(0f, offset * i, 0);
                
                var btText = g.GetComponentInChildren<TMP_Text>();
                btText.text = $"Team {t.GetTeamName()} Won!";
            }
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