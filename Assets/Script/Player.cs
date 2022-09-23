using System;
using UnityEngine;

public class EntityLife
{
    public bool IsAlive => Life > 0;
    public int Life { get; set; }
    public int MaxLife { get; set; }

    /// <summary>
    /// EntityLife(life), int(damage)
    /// </summary>
    public event Action<EntityLife, int> LifeUpdatedEvent;
    public event Action DeathEvent;

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

        Life -= dmg;
        Debug.Log($"took dmg: {dmg}  ({Life}hp)");
        LifeUpdatedEvent?.Invoke(this, -dmg);
        if(!IsAlive)
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
        Debug.Log($"heal: +{val}  ({Life}hp)");
        LifeUpdatedEvent?.Invoke(this, val);
    }

    public void Kill()
    {
        Life = 0;
        DeathEvent?.Invoke();
    }
    
}

public class Player : GameActor
{
    private const int PLAYER_START_LIFE = 100;

    // public PlayerControllerManager ControlManager;
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
        // ControlManager = GetComponent<PlayerControllerManager>();
    }

    public void Init(Team team)
    {
        _team = team;
        _id = _team.Players.Count;
    }

    protected override void Update()
    {
        base.Update();
        // IsControllersEnabled = ControlManager.ControllersEnabled;
    }
}