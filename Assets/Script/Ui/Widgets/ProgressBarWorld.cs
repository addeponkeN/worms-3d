using UnityEngine;

namespace Ui.Widgets
{
    public class ProgressBarWorld : ProgressBar
    {
        public Transform Target;
        public Vector3 Offset;

        public bool FaceView = true;

        public void SetTarget(Transform tf)
        {
            SetTarget(tf, Vector3.zero);
        }

        public void SetTarget(Transform tf, Vector3 offset)
        {
            Target = tf;
            Offset = offset;
        }

        protected override void Update()
        {
            base.Update();

            if(Target == null)
                return;

            transform.position = Target.position + Offset;

            if(FaceView)
            {
                TfRect.rotation = Quaternion.LookRotation(-Camera.main.transform.forward);
            }
        }
    }
}