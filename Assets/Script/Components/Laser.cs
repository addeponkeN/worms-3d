using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(LineRenderer))]
    public class Laser : MonoBehaviour
    {
        private LineRenderer _ren;
        private Vector3[] _positions;

        public void SetTarget(Vector3 pos)
        {
            _positions[1] = pos;
            _ren.SetPositions(_positions);
        }
    
        public void SetActivated(bool isActivated) => gameObject.SetActive(isActivated);
    
        private void Awake()
        {
            _ren = GetComponent<LineRenderer>();
            _positions = new Vector3[2];
            _ren.SetPositions(_positions);
        }

        private void Update()
        {
            _positions[0] = transform.position;
            _ren.SetPositions(_positions);
        }
    }
}
