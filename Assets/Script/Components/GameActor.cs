using System;
using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(CharacterController))]
    public class GameActor : MonoBehaviour
    {
        private const int DEFAULT_LIFE = 100;

        [NonSerialized] public CharacterController CharController;
        [NonSerialized] public SimpleBody Body;

        public StatContainer Stats;
        public ActorLife Life;

        protected EnvironmentInteractor EnvInteractor;

        protected virtual void Awake()
        {
            CharController = GetComponent<CharacterController>();
            EnvInteractor = GetComponent<EnvironmentInteractor>();
            CharController.radius = 0.1f;
            Life = new ActorLife(this, DEFAULT_LIFE);
            Body = gameObject.AddComponent<SimpleBody>();
        }

        protected virtual void Start()
        {
            EnvInteractor.ExplosionEvent += OnExplosionEvent;
            gameObject.AddComponent<LifeBoundsComponent>();
        }

        protected virtual void OnExplosionEvent(ExplodeData data)
        {
        }

        protected virtual void Update()
        {
        }

        protected virtual void FixedUpdate()
        {
        }

        protected virtual void LateUpdate()
        {
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
}