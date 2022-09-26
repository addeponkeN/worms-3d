using System;
using Teams;
using UnityEngine;

public class ActorLife
{
    public GameActor Parent;
    
    public bool IsAlive => Life > 0;
    public int Life { get; set; }
    public int MaxLife { get; set; }

    /// <summary>
    /// EntityLife(life), int(damage)
    /// </summary>
    public event Action<ActorLife, int> LifeUpdatedEvent;
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
        LifeUpdatedEvent?.Invoke(this, -dmg);
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
        LifeUpdatedEvent?.Invoke(this, val);
    }

    public void Kill()
    {
        Life = 0;
        DeathEvent?.Invoke(Parent);
    }
    
}

public class Player : GameActor
{
    public Transform CameraPosition;
    public Transform CameraAimPosition;
    public Transform WeaponOrigin;


    private TeamBanner _banner;
    private Team _team;
    private int _id;

    protected override void Awake()
    {
        base.Awake();
        _banner = GetComponentInChildren<TeamBanner>();
        CharController = GetComponent<CharacterController>();
    }

    public Team GetTeam() => _team;

    public void AssignTeam(Team team)
    {
        _team = team;
        _banner.SetColor(team.GetColor());
        _id = _team.Players.Count;
    }

}