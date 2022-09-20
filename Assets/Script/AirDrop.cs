using GameStates;
using UnityEngine;
using Util;
using VoxelEngine;

public class AirDrop : MonoBehaviour, IFollowable
{
    private static readonly int Idle = Animator.StringToHash("Idle");
    private const float Gravity = 5f;

    public bool EndFollow { get; set; }

    private float _waitTimer = 1.5f;

    [SerializeField] private GameObject _crate;
    [SerializeField] private GameObject _parachute;
    private Animator _animator;

    private bool _landed;

    private void Awake()
    {
        _animator = GetComponent<Animator>();
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

    private void Land()
    {
        _landed = true;
        
        //  cleanup
        Destroy(_parachute);
        Destroy(GetComponent<BoxCollider>());
        Destroy(GetComponent<Rigidbody>());
        
        _crate.GetComponent<BoxCollider>().enabled = true;
        var body = _crate.AddComponent<Rigidbody>();
        body.useGravity = true;
        body.mass = 8f;

        _animator.SetTrigger(Idle);
    }

    private void Crash()
    {
        _landed = true;
        Destroy(gameObject);
    }

    private void Update()
    {
        if(!_landed)
        {
            transform.Translate(0f, -Gravity * Time.deltaTime, 0f);
            if(transform.position.y < World.Get.WaterLevel)
            {
                Crash();
            }
        }
        else
        {
            _waitTimer -= Time.deltaTime;
            if(_waitTimer <= 0)
            {
                EndFollow = true;
            }
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.layer == LayerMask.NameToLayer("World"))
        {
            Land();
        }
    }
}