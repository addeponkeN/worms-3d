using Components;
using Ui.Widgets;
using UnityEngine;

namespace Ui
{
    public class PlayerUi : MonoBehaviour
    {
        private Player _player;
        public ProgressBarWorld HealthBar;

        private void Awake()
        {
            var ui = GameManager.Get.Ui.World;
            var prefBar = PrefabManager.Get.GetPrefab("progressbar_player");
            HealthBar = Instantiate(prefBar, ui.transform)
                .GetComponent<ProgressBarWorld>();
        }

        public void Init(Player player)
        {
            _player = player;
        
            HealthBar.ImgFill.color = _player.GetTeam().GetColor();
            HealthBar.SetTarget(_player.transform, new Vector3(0, 2f, 0));
            HealthBar.Value = 1f;
        
            _player.Life.LifeChangedEvent += LifeOnLifeChangedEvent;
            _player.Life.DeathEvent += LifeOnDeathEvent;
        }

        private void LifeOnDeathEvent(GameActor obj)
        {
            Destroy(HealthBar.gameObject);
        }

        private void LifeOnLifeChangedEvent(ActorLife player, int damage)
        {
            HealthBar.Value = player.LifePercentage;
        }

    }
}