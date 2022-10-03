using System;
using System.Collections.Generic;
using AudioSystem;
using CameraSystem;
using GameStates;
using GameSystems;
using Ui;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.SceneManagement;

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
            if (_isGamePaused)
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
        if (!GameCore.IsInited)
        {
            SceneManager.LoadScene("Scenes/MenuScene");
            return;
        }
        
        if (Get == null)
        {
            Get = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        SetupGame();
    }

    private void SetupGame()
    {
        PlayerInput = GetComponent<PlayerInput>();

        StateManager = new GameStateManager();
        StateManager.PushState(new GameStateLoading());

        Systems = new();

        var rules = GameCore.Get.GameRules;

        if (rules.EnableAirdrops)
            AddGameSystem(new AirDropSystem());
        if (rules.EnableDangerZone)
            AddGameSystem(new WaterLevelSystem());
    }

    public GameState GetGameState()
    {
        return StateManager.CurrentState;
    }

    private void Start()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Confined;

        AudioManager.PlayMusic("gameplay1");
    }

    private void AddGameSystem(GameSystem system)
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

    private void OnMouseEnter()
    {
        Cursor.visible = false;
    }

    private void OnMouseExit()
    {
        Cursor.visible = true;
    }
}