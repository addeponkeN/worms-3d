using System;
using System.Collections.Generic;
using CameraSystem;
using GameStates;
using GameSystems;
using UnityEngine;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Get { get; private set; }

    [NonSerialized] public CameraManager CamManager;
    [NonSerialized] public PlayerManager PlayerManager;
    [NonSerialized] public UiManager Ui;
    
    [NonSerialized]
    public PlayerInput PlayerInput;
    
    public List<GameSystem> Systems;

    private GameStateManager _stateManager;

    private void Awake()
    {
        if(Get == null)
        {
            Get = this;
        }
        
        PlayerInput = GetComponent<PlayerInput>();
        
        _stateManager = new GameStateManager();
        _stateManager.PushState(new GameStateLoading());

        Systems = new();
        // AddGameSystem(new AirDropSystem());
    }

    void AddGameSystem(GameSystem system)
    {
        system.GameStateManager = _stateManager;
        Systems.Add(system);
    }

    public void SetupManagers()
    {
        CamManager = gameObject.AddComponent<CameraManager>();
        PlayerManager = gameObject.AddComponent<PlayerManager>();
        Ui = gameObject.AddComponent<UiManager>();
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