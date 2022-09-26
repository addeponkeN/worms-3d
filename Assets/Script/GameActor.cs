using System;
using EntityComponents;
using UnityEngine;

[RequireComponent(typeof(CharacterController), typeof(ExplosionTargetObject))]
public class GameActor : MonoBehaviour
{
    private const int DEFAULT_LIFE = 100;

    [NonSerialized] public CharacterController CharController;

    public GameObject Model;
    public StatContainer Stats;
    public ActorLife Life;
    public SimpleBody Body;
    protected ExplosionTargetObject ExploTarget;

    protected virtual void Awake()
    {
        CharController = GetComponent<CharacterController>();
        ExploTarget = GetComponent<ExplosionTargetObject>();
        CharController.radius = 0.1f;
        Model = transform.Find("Model").gameObject;
        Life = new ActorLife(this, DEFAULT_LIFE);
        Body = gameObject.AddComponent<SimpleBody>();
    }

    protected virtual void Start()
    {
        ExploTarget.DamagedEvent += OnExplosionDamagedEvent;
        gameObject.AddComponent<LifeBoundsComponent>();
    }

    protected virtual void OnExplosionDamagedEvent(ExplodeData data)
    {
    }

    protected virtual void Update()
    {
        Body.Update();
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void LateUpdate()
    {
        Body.LateUpdate();
    }

    public virtual void Kill()
    {
        DestroySelf();
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
    
}

