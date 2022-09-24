using System;
using EntityComponents;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GameActor : MonoBehaviour
{
    private const int DEFAULT_LIFE = 100;

    [NonSerialized] public CharacterController CharController;

    public GameObject Model;
    public StatContainer Stats;
    public ActorLife Life;

    public SimpleBody Body;

    protected virtual void Awake()
    {
        CharController = GetComponent<CharacterController>();
        CharController.radius = 0.1f;
        Model = transform.Find("Model").gameObject;
        Life = new ActorLife(this, DEFAULT_LIFE);
        Body = gameObject.AddComponent<SimpleBody>();
    }

    protected virtual void Start()
    {
        gameObject.AddComponent<LifeBoundsComponent>();
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
        // Invoke(nameof(DestroySelf));
        DestroySelf();
    }

    protected virtual void DestroySelf()
    {
        Destroy(gameObject);
    }
    
}

