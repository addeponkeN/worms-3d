using GameStates;
using TMPro;
using Ui;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lbPlayTimer;

    public UiWeaponBar WeaponBar;

    private void Start()
    {
        GameManager.Get.StateManager.GameStateChangedEvent += StateManagerOnGameStateChangedEvent;
        GameManager.Get.PlayerManager.WeaponChangedEvent += PlayerManagerOnWeaponChangedEvent;
    }

    private void PlayerManagerOnWeaponChangedEvent()
    {
        
    }

    private void StateManagerOnGameStateChangedEvent(GameState state)
    {
        if(state is GameStateActivePlayer playState)
        {
        }
    }

    private void Update()
    {
        var state = GameManager.Get.GetGameState();

        if(state is GameStateActivePlayer player)
        {
            lbPlayTimer.text = $"{(int)player.PlayTimer}";
        }
    }
}