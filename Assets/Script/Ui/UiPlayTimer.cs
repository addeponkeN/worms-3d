using GameStates;
using TMPro;
using UnityEngine;

namespace Ui
{
    public class UiPlayTimer : MonoBehaviour
    {
        private TextMeshProUGUI _label;
        private GameStateActivePlayer _playState;

        private void Awake()
        {
            _label = GetComponent<TextMeshProUGUI>();
        }

        private void Start()
        {
            gameObject.SetActive(false);
            GameManager.Get.StateManager.GameStateChangedEvent += StateManagerOnGameStateChangedEvent;
        }

        private void StateManagerOnGameStateChangedEvent(GameState state)
        {
            if(state is GameStateActivePlayer playState)
            {
                _playState = playState;
                gameObject.SetActive(true);
                UpdateText();
            }
            else
            {
                gameObject.SetActive(false);
            }
        }

        private void UpdateText()
        {
            _label.text = $"{(int)_playState.PlayTimer}";
        }

        private void Update()
        {
            UpdateText();
        }
    }
}