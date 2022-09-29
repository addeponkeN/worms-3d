using UnityEngine;

namespace Components
{
    [RequireComponent(typeof(EnvironmentInteractor))]
    public class EnvironmentObject : MonoBehaviour
    {
        private void Awake()
        {
            GetComponent<EnvironmentInteractor>().ExplosionEvent += OnExplosionEvent;
        }

        private void OnExplosionEvent(ExplodeData obj)
        {
            Destroy(gameObject);
        }
    }
}