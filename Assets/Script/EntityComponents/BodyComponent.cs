using UnityEngine;

namespace EntityComponents
{
    // public class BodyComponent : BaseEntityComponent
    // {
    //     private float _velocity;
    //     private float _maxVel = 5f;
    //
    //     public override void Init()
    //     {
    //         base.Init();
    //     }
    //
    //     public override void Update()
    //     {
    //         if(_velocity < _maxVel)
    //             _velocity += Time.deltaTime * Ent.Stats.Gravity;
    //         if(_velocity > _maxVel)
    //             _velocity = _maxVel;
    //
    //         Ent.CharController.Move(Vector3.down * _velocity);
    //         
    //         if(Ent.CharController.isGrounded)
    //         {
    //             _velocity = 0f;
    //         }
    //     }
    //
    //     public override void FixedUpdate()
    //     {
    //     }
    //
    //     public void AddForce(float f)
    //     {
    //         _velocity -= f;
    //     }
    //
    //     public void SetForce(float f)
    //     {
    //         _velocity -= f;
    //     }
    // }
}