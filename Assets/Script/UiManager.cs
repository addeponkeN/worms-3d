using System;
using GameStates;
using UnityEngine;

public class UiManager : MonoBehaviour, ILoader
{
    public Canvas AimCanvas;
    public Canvas MainCanvas;

    private void Awake()
    {
        AimCanvas = GameObject.Find("AimCanvas").GetComponent<Canvas>();
        MainCanvas = GameObject.Find("MainCanvas").GetComponent<Canvas>();
    }

    void Start()
    {
        
    }

    public void Load()
    {
        
    }
    
    void Update()
    {
        
    }

}
