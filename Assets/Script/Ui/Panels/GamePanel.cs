using GameStates;
using TMPro;
using UnityEngine;

namespace Ui
{
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
}