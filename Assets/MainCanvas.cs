using System.Collections.Generic;
using Ui;
using UnityEngine;

public interface IUiPanel
{
    MainCanvas Main { get; set; }
    GameObject gameObject { get; }
    void OnFocused(bool isFocused);
    void OnRemoved();
}

public class MainCanvas : MonoBehaviour
{
    private List<IUiPanel> _registeredPanels;
    private Stack<IUiPanel> _panelStack;

    private void Awake()
    {
        _panelStack = new Stack<IUiPanel>();
        _registeredPanels = new List<IUiPanel>();
    }

    private void Start()
    {
        var uiPanels = PrefabManager.Get.GetPrefabs("Ui/Panels/");
        for(int i = 0; i < uiPanels.Length; i++)
        {
            var panelGo = uiPanels[i];
            var go = Instantiate(panelGo, transform);
            _registeredPanels.Add(go.GetComponent<IUiPanel>());
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
    
    public void PushPanel(IUiPanel panel)
    {
        if(_panelStack.Count > 0)
        {
            SetEnableCurrent(false);
        }
        panel.Main = this;
        panel.gameObject.SetActive(true);
        panel.OnFocused(true);
        _panelStack.Push(panel);
    }

    void SetEnableCurrent(bool isEnabled)
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
        SetEnableCurrent(true);
    }
    
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.Escape))
        {
            if(_panelStack.Count <= 0)
            {
                PushPanel<PausePanel>();
            }
            else
            {
                ExitPanel();
            }
        }
    }
}