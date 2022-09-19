using UnityEngine;

namespace PlayerControllers
{
    public class PlayerGravityController : BasePlayerController
    {
        private float _velocity;
        private float _maxVel = 5f;

        public override void Update()
        {
            if(_velocity < _maxVel)
                _velocity += Time.deltaTime * Manager.Stats.Gravity;
            if(_velocity > _maxVel)
                _velocity = _maxVel;
        }

        public override void FixedUpdate()
        {
            Manager.CharController.Move(Vector3.down * _velocity);
            // transform.position += Vector3.down * _velocity;

            if(Manager.CharController.isGrounded)
            {
                _velocity = 0f;
            }
        }

        public void AddForce(float f)
        {
            _velocity -= f;
        }

        public void SetForce(float f)
        {
            _velocity -= f;
        }
    }
}