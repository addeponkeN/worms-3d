using System;
using GameStates;
using UnityEngine;
using Util;
using VoxelEngine;

namespace Components
{
    public enum CrateItemTypes
    {
        Health,
    }

    public class AirDrop : MonoBehaviour, IFollowable
    {
        private static readonly int Idle = Animator.StringToHash("Idle");
        private const float FallSpeed = 6f;

        public bool EndFollow { get; set; }
        public Transform Transform => _collider.transform;
    
        [NonSerialized] public bool ReleasedParachute;

        [SerializeField] private GameObject _parachute;
        [SerializeField] private GameObject _collider;
        [SerializeField] private GameObject _playerCollider;
    
        private float _waitTimer = 1.5f;
        private Animator _animator;

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

        public void LootItem(Player player)
        {
            //  temp

            var rewardType = CrateItemTypes.Health;
        
            switch(rewardType)
            {
                case CrateItemTypes.Health:
                    player.Life.Heal(50);
                    break;
            }
        
            Debug.Log("gave player loot. time to die");
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

            if(transform.position.y < World.Get.Water.WaterLevel)
            {
                Crash();
            }
        }

        private void OnTriggerEnter(Collider other)
        {
            if(other.gameObject.layer == LayerMask.NameToLayer("World"))
            {
                ReleaseParachute();
            }
        }
    }
}