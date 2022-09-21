using System;
using GameStates;
using UnityEngine;
using Util;
using VoxelEngine;

public class AirDrop : MonoBehaviour, IFollowable
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private const float FallSpeed = 6f;

    public bool EndFollow { get; set; }
    public Transform Transform => _collider.transform;
    [NonSerialized] public bool ReleasedParachute = false;

    private float _waitTimer = 1.5f;

    private Animator _animator;
    [SerializeField] private GameObject _parachute;
    [SerializeField] private GameObject _collider;
    
    private bool _landed;

    private void Awake()
    {
        _animator = GetComponentInChildren<Animator>();
    }

    private void Start()
    {
        FindSafePosition();
    }

    public void FindSafePosition()
    {
        var position = World.Get.GetRandomSafePosition();
        position.y = World.ChunkSize.y + 5;
        transform.position = position;
    }

    public void ReleaseParachute()
    {
        ReleasedParachute = true;
        Destroy(_parachute);
        Destroy(GetComponent<BoxCollider>());
        Destroy(GetComponent<Rigidbody>());

        var boxCollider = _collider.GetComponent<BoxCollider>();
        boxCollider.enabled = true;
        var body = _collider.GetComponent<Rigidbody>();
        body.useGravity = true;
        body.mass = 1f;
        
        _animator.SetTrigger(Idle);
    }

    private void Crash()
    {
        ReleasedParachute = true;
        Destroy(gameObject);
    }

    private void Update()
    {
        if(!ReleasedParachute)
        {
            transform.Translate(0f, -FallSpeed * Time.deltaTime, 0f);
        }

        if(ReleasedParachute)
        {
            _waitTimer -= Time.deltaTime;
            if(_waitTimer <= 0)
            {
                EndFollow = true;
            }
        }

        if(transform.position.y < World.Get.WaterLevel)
        {
            Crash();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            _landed = true;
            ReleaseParachute();
        }
    }
}