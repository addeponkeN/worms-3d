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

    private bool _isGamePaused;
    public bool IsGamePaused
    {
        get => _isGamePaused;
        set
        {
            _isGamePaused = value;
            if(_isGamePaused)
                Time.timeScale = 0f;
            else
                Time.timeScale = 1f;
        }
    }

    [NonSerialized] public CameraManager CamManager;
    [NonSerialized] public PlayerManager PlayerManager;
    [NonSerialized] public UiManager Ui;
    [NonSerialized] public PlayerInput PlayerInput;
    
    public GameStateManager StateManager;
    public List<GameSystem> Systems;

    private void Awake()
    {
        if(Get == null)
        {
            Get = this;
        }
        
        PlayerInput = GetComponent<PlayerInput>();
        
        StateManager = new GameStateManager();
        StateManager.PushState(new GameStateLoading());

        Systems = new();
        // AddGameSystem(new AirDropSystem());
        AddGameSystem(new WaterLevelSystem());
    }

    public GameState GetGameState()
    {
        return StateManager.CurrentState;
    }

    private void Start()
    {
        Cursor.visible = false;
    }
    
    void AddGameSystem(GameSystem system)
    {
        system.GameStateManager = StateManager;
        Systems.Add(system);
    }

    public void SetupManagers()
    {
        CamManager = gameObject.AddComponent<CameraManager>();
        PlayerManager = gameObject.AddComponent<PlayerManager>();
        Ui = gameObject.AddComponent<UiManager>();
    }

    private void Update()
    {
        StateManager.Update();
    }

    private void FixedUpdate()
    {
        StateManager.FixedUpdate();
    }


}