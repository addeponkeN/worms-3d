using UnityEngine;

namespace CameraSystem.CameraStates
{
    public class WorldOverviewState : ICameraState
    {
        public Camera Camera { get; set; }
        public bool IsAlive { get; set; }
        public CameraManager Manager { get; set; }

        public void Init()
        {
        }

        public void Update()
        {
        }

        public void FixedUpdate()
        {
        }

        public void OnExit()
        {
        }
    }
}