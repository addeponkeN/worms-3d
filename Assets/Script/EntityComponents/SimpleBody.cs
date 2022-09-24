using UnityEngine;

namespace EntityComponents
{
    [RequireComponent(typeof(CharacterController))]
    public class SimpleBody : BaseEntityComponent
    {
        private CharacterController _con;

        private float _fallForce;
        private float _force;
        private float _gravity;
        private Vector3 _pushDir;
        private float _mass = 1f;
        private float _jumpMass = 6f;

        public override void Start()
        {
            base.Start();
            _con = gameObject.GetComponent<CharacterController>();
        }

        public void Push(Vector3 direction, float force)
        {
            _pushDir = direction * force;
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
                _fallForce -= _con.isGrounded ? (dt * _mass * 3f) : (dt * _mass);
                forceDir.y = _fallForce * _pushDir.y;
            }

            if(_force > 0.01f)
            {
                if(_con.isGrounded)
                {
                    _force -= dt * (_mass * 4f);
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
}