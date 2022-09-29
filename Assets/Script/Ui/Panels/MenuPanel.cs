using UnityEngine;

namespace Ui
{
    public abstract class MenuPanel : MonoBehaviour, IUiPanel
    {
        public MainCanvas Main { get; set; }
        public abstract void OnFocused(bool isFocused);
        public abstract void OnRemoved();
    }
}