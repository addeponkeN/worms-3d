using GameStates;
using UnityEngine;

namespace Ui
{
    public class UiManager : MonoBehaviour, ILoader
    {
        public Canvas AimCanvas;
        public MainCanvas MainCanvas;
        public GamePanel GamePanel;

        private void Awake()
        {
            AimCanvas = GameObject.Find("AimCanvas").GetComponent<Canvas>();
            var mainCanvasGo = GameObject.Find("MainCanvas");
            MainCanvas = mainCanvasGo.GetComponent<MainCanvas>();
            GamePanel = mainCanvasGo.GetComponentInChildren<GamePanel>();
            
            AimCanvas.gameObject.SetActive(false);
        }
    
        public void Load()
        {
            
        }
    
    }
}
