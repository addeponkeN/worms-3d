using System;
using CameraSystem;
using GameStates;
using PlayerControllers;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Get { get; private set; }

    public CameraManager CamManager;
    public PlayerControllerManager ControllerManager;
    public PlayerManager PlayerManager;
    public UiManager Ui;

    private GameStateManager _stateManager;

    private void Awake()
    {
        if(Get == null)
        {
            Get = this;
        }

        _stateManager = new GameStateManager();
        _stateManager.PushState(new GameStateLoading());
    }

    private void Start()
    {
    }

    private void Update()
    {
        _stateManager.Update();
    }

    private void FixedUpdate()
    {
        _stateManager.FixedUpdate();
    }
}