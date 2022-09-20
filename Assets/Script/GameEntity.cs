using System;
using EntityComponents;
using UnityEngine;

[RequireComponent(typeof(CharacterController))]
public class GameEntity : MonoBehaviour
{
    private const int DEFAULT_LIFE = 100;

    [NonSerialized] public CharacterController CharController;
    [NonSerialized] public ComponentManager ComponentManager;

    public GameObject Model;
    public bool IsGrounded;
    public StatContainer Stats;
    public EntityLife Life;

    public SimpleBody Body;

    protected virtual void Awake()
    {
        CharController = GetComponent<CharacterController>();
        ComponentManager = gameObject.AddComponent<ComponentManager>();
        Model = GameObject.Find("Model");
        Life = new EntityLife(DEFAULT_LIFE);
        Body = new SimpleBody(CharController);
    }

    protected virtual void Update()
    {
        IsGrounded = CharController.isGrounded;
        Body.Update();
    }

    protected virtual void FixedUpdate()
    {
    }

    protected virtual void LateUpdate()
    {
        Body.LateUpdate();
    }
}

public class SimpleBody : BaseEntityComponent
{
    private CharacterController _con;

    private float _fallForce;
    private float _force;
    private float _gravity;
    private Vector3 _pushDir;
    private float _mass = 2f;
    private float _jumpMass = 6f;

    public SimpleBody(CharacterController con)
    {
        _con = con;
    }

    public void Push(Vector3 direction, float force)
    {
        _pushDir = direction.normalized * force;
        _force = force;
        _fallForce = force;
    }

    public void Jump(float amount)
    {
        _gravity = amount;
    }

    public void Move(Vector3 amount)
    {
        amount.y -= 0.00001f;
        _con.Move(amount);
    }

    public override void Update()
    {
        var dt = Time.deltaTime;

        Vector3 forceDir = Vector3.zero;

        if(_fallForce > 0.01f)
        {
            _fallForce -= dt * _mass;
            forceDir.y = _fallForce * _pushDir.y;
        }

        if(_force > 0.01f)
        {
            if(_con.isGrounded)
            {
                _force -= dt * (_mass * 2.25f);
            }
            else
            {
                _force -= dt * (_mass * 0.1f);
            }

            forceDir.x = _force * _pushDir.x;
            forceDir.z = _force * _pushDir.z;
        }

        _gravity -= dt * _jumpMass;
        forceDir.y += _gravity;

        if(forceDir.y == 0f)
            forceDir.y = -0.0000001f;

        Move(forceDir * dt);

        if(_con.isGrounded)
            _gravity = 0f;
    }

    public void LateUpdate()
    {
    }
}