using System;
using System.Collections.Generic;
using UnityEngine;

namespace Ui
{
    public interface IUiPanel
    {
        MainCanvas Main { get; set; }
        GameObject gameObject { get; }
        void OnFocused(bool isFocused);
        void OnRemoved();
    }

    public abstract class MenuPanel : MonoBehaviour, IUiPanel
    {
        public MainCanvas Main { get; set; }
        public abstract void OnFocused(bool isFocused);
        public abstract void OnRemoved();
    }

    public class MainCanvas : MonoBehaviour
    {
        [SerializeField] private MenuPanel DefaultPanelPrefab;

        public bool KeepBottomPanel;

        private List<IUiPanel> _registeredPanels;
        private Stack<IUiPanel> _panelStack;

        private void Awake()
        {
            _panelStack = new Stack<IUiPanel>();
            _registeredPanels = new List<IUiPanel>();
        }

        private void Start()
        {
            RegisterAllPrefabPanels();
        }

        private void RegisterAllPrefabPanels()
        {
            var uiPanels = PrefabManager.GetPrefabs("Ui/Panels/");
            for(int i = 0; i < uiPanels.Length; i++)
            {
                var panelGo = uiPanels[i];
                var go = Instantiate(panelGo, transform);
                var uiPanel = go.GetComponent<IUiPanel>();
                if(uiPanel == null)
                {
                    Debug.LogWarning($"UiPanel does not inherit IUiPanel: {panelGo.name}");
                    continue;
                }

                _registeredPanels.Add(uiPanel);
                go.SetActive(false);
            }
        }

        public void PushPanel<T>() where T : IUiPanel
        {
            for(int i = 0; i < _registeredPanels.Count; i++)
                if(_registeredPanels[i] is T ret)
                {
                    PushPanel(ret);
                    return;
                }

            Debug.LogError($"Could not find panel type: {typeof(T).Name}");
        }

        public void PushPanel(Type type)
        {
            if(type == null)
            {
                Debug.LogError("Panel type was null");
                return;
            }

            for(int i = 0; i < _registeredPanels.Count; i++)
            {
                var regType = _registeredPanels[i].GetType();
                if(regType == type)
                {
                    PushPanel(_registeredPanels[i]);
                    return;
                }
            }

            Debug.LogError($"Could not find panel type: {type.Name}");
        }

        public void PushPanel(IUiPanel panel)
        {
            if(_panelStack.Count > 0)
            {
                SetCurrentIsEnabled(false);
            }

            panel.Main = this;
            panel.gameObject.SetActive(true);
            panel.OnFocused(true);
            _panelStack.Push(panel);
        }

        private void SetCurrentIsEnabled(bool isEnabled)
        {
            if(_panelStack.Count <= 0)
                return;

            var currentPanel = _panelStack.Peek();
            currentPanel.gameObject.SetActive(isEnabled);
            currentPanel.OnFocused(isEnabled);
        }

        public void ExitPanel()
        {
            var current = _panelStack.Pop();
            current.gameObject.SetActive(false);
            current.OnFocused(false);
            current.OnRemoved();
            SetCurrentIsEnabled(true);
        }

        private void Update()
        {
            if(Input.GetKeyDown(KeyCode.Escape))
            {
                if(_panelStack.Count <= 0)
                {
                    PushPanel(DefaultPanelPrefab.GetType());
                }
                else if(_panelStack.Count == 1 && !KeepBottomPanel)
                {
                    ExitPanel();
                }
            }
        }
    }
}