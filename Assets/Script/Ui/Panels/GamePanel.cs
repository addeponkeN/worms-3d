using GameStates;
using TMPro;
using Ui;
using UnityEngine;

public class GamePanel : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI lbPlayTimer;

    public UiWeaponBar WeaponBar;

    private void Update()
    {
        var state = GameManager.Get.GetGameState();

        if(state is GameStateActivePlayer player)
        {
            lbPlayTimer.text = $"{(int)player.PlayTimer}";
        }
    }
}