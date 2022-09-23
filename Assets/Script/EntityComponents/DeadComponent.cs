using UnityEngine;
using Util;

namespace EntityComponents
{
    public class DeadComponent : BaseEntityComponent
    {
        private Timer _timer;

        public override void Start()
        {
            base.Start();
            _timer = 0.01f;
        }

        public override void Update()
        {
            base.Update();

            if(_timer.Update())
            {
                Finish();
            }
            
        }

        public void Finish()
        {
            Object.Destroy(Ent.gameObject);
        }
    }
}