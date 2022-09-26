using GameStates;
using UnityEngine;

namespace Ui
{
    public class UiManager : MonoBehaviour, ILoader
    {
        public Canvas AimCanvas;
        public Canvas MainCanvas;

        private void Awake()
        {
            AimCanvas = GameObject.Find("AimCanvas").GetComponent<Canvas>();
            MainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
        
            AimCanvas.gameObject.SetActive(false);
        }
    
        public void Load()
        {
        }
    
    }
}
