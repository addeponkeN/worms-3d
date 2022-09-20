using System;
using PlayerControllers;
using UnityEngine;

public class EntityLife
{
    public bool IsAlive => Life > 0;
    public int Life { get; set; }
    public int MaxLife { get; set; }

    /// <summary>
    /// Life and Damage amount
    /// </summary>
    public Action<EntityLife, int> LifeUpdatedEvent;
    public Action DeathEvent;

    public EntityLife(int life)
    {
        Life = life;
        MaxLife = life;
    }

    public void TakeDamage(int dmg)
    {
        if(dmg == 0)
        {
            return;
        }

        Debug.Log($"took dmg: {dmg}");

        Life -= dmg;
        LifeUpdatedEvent?.Invoke(this, -dmg);
        if(Life <= 0)
        {
            DeathEvent?.Invoke();
        }
    }

    public void Heal(int val)
    {
        if(val == 0)
        {
            return;
        }

        Life = Mathf.Clamp(Life + val, 0, MaxLife);
        LifeUpdatedEvent?.Invoke(this, val);
    }
    
}

public class Player : GameEntity
{
    private const int PLAYER_START_LIFE = 100;

    public PlayerControllerManager ControlManager;
    public bool IsControllersEnabled;

    public Transform CameraPosition;
    public Transform CameraAimPosition;
    public Transform WeaponOrigin;

    private Team _team;
    private int _id;

    protected override void Awake()
    {
        base.Awake();
        CharController = GetComponent<CharacterController>();
        ControlManager = GetComponent<PlayerControllerManager>();
    }

    public void Init(Team team)
    {
        _team = team;
        _id = _team.Players.Count;
    }

    protected override void Update()
    {
        base.Update();
        IsControllersEnabled = ControlManager.ControllersEnabled;
    }
}