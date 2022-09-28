using System;
using Teams;
using UnityEngine;
using ProgressBar = Ui.Widgets.ProgressBar;

public class ActorLife
{
    public GameActor Parent;
    
    public bool IsAlive => Life > 0;
    public int Life { get; set; }
    public int MaxLife { get; set; }

    public float LifePercentage => (float)Life / MaxLife;

    /// <summary>
    /// EntityLife(life), int(damage)
    /// </summary>
    public event Action<ActorLife, int> LifeChangedEvent;
    public event Action<GameActor> DeathEvent;

    public ActorLife(GameActor actor, int life)
    {
        Parent = actor;
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
        LifeChangedEvent?.Invoke(this, -dmg);
        if(!IsAlive)
        {
            DeathEvent?.Invoke(Parent);
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
        LifeChangedEvent?.Invoke(this, val);
    }

    public void Kill()
    {
        Life = 0;
        DeathEvent?.Invoke(Parent);
    }
    
}

public class Player : GameActor
{
    [NonSerialized] public PlayerUi Ui;
    public Transform CameraPosition;
    public Transform CameraAimPosition;
    public Transform WeaponOrigin;

    private Team _team;
    private int _playerId;
    private ProgressBar _bar;

    protected override void Awake()
    {
        base.Awake();
        CharController = GetComponent<CharacterController>();
        Ui = gameObject.AddComponent<PlayerUi>();
    }

    public Team GetTeam() => _team;
    public int GetPlayerId() => _playerId;

    public void AssignTeam(Team team)
    {
        _team = team;
        _playerId = team.Players.Count;
        Ui.Init(this);
    }

    protected override void OnExplosionEvent(ExplodeData data)
    {
        base.OnExplosionEvent(data);

        var distance = Vector3.Distance(transform.position, data.Position);
        var multiplier = Mathf.Clamp(1.15f - distance / data.Radius, 0.1f, 1f);
        var finalDamage = (int)(multiplier * data.Damage);
        Life.TakeDamage(finalDamage);

        Debug.Log($"player took dmg from explo: {finalDamage}");

        var force = data.Damage / 10f * multiplier;
        var dir = ((transform.position - data.Position).normalized + Vector3.up).normalized;
        Body.Push(dir, force);
    }
}