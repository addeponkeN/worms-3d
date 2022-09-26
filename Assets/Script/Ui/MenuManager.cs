using System;
using Ui;
using UnityEngine;

public class MenuManager : MonoBehaviour
{
    [SerializeField] private MainCanvas MainCanvas;

    private bool _inited;

    private void Update()
    {
        if(!_inited)
        {
            _inited = true;
            MainCanvas.PushPanel<MainMenuPanel>();
        }
    }
}