using UnityEngine;

namespace CameraSystem.CameraStates
{
    public interface ICameraState
    {
        Camera Camera { get; set; }
        bool IsAlive { get; set; }
        CameraManager Manager { get; set; }
        void Init();
        void Update();
        void FixedUpdate();
        void OnExit();
    }
}