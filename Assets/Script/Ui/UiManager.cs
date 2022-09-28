using GameStates;
using UnityEngine;

namespace Ui
{
    public class UiManager : MonoBehaviour, ILoader
    {
        public Canvas AimCanvas;
        public GamePanel GamePanel;
        public MainCanvas Main;
        public WorldCanvas World;

        private void Awake()
        {
            AimCanvas = GameObject.Find("AimCanvas").GetComponent<Canvas>();
            var mainGo = GameObject.Find("MainCanvas");
            Main = mainGo.GetComponent<MainCanvas>();
            GamePanel = mainGo.GetComponentInChildren<GamePanel>();
            
            var worldGo = GameObject.Find("WorldCanvas");
            World = worldGo.GetComponent<WorldCanvas>();
            
            AimCanvas.gameObject.SetActive(false);
        }
    
        public void Load()
        {
            
        }
    
    }
}
