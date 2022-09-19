using System.Collections.Generic;
using CameraSystem.CameraStates;
using Cinemachine;
using UnityEngine;
using VoxelEngine;

namespace CameraSystem
{
    public class CameraManager : MonoBehaviour
    {
        public Camera Camera;
        public CinemachineVirtualCamera MoveCam;
        public CinemachineVirtualCamera AimCam;
        public ICameraState MainState;

        public Transform DefaultPosition;
        
        private List<ICameraState> _states;
        private Queue<ICameraState> _queue;

        private void Awake()
        {
            _states = new List<ICameraState>();
            _queue = new Queue<ICameraState>();
        }

        private void Start()
        {
            AddDefaultState();
            
            World.Get.OnGeneratedEvent += GetOnOnGeneratedEvent;
            
        }

        private void GetOnOnGeneratedEvent()
        {
            
        }

        public void SetMainState(ICameraState state)
        {
            if(MainState != null)
            {
                MainState.OnExit();
            }
            
            MainState = state;
            MainState.Camera = Camera;
            MainState.IsAlive = true;
            MainState.Manager = this;
            MainState.Init();
        }

        public void AddDefaultState()
        {
            SetMainState(new FollowPlayerState(GameManager.Get.ControllerManager.CurrentPlayer));
        }

        public void QueueMainState(ICameraState state)
        {
            _queue.Enqueue(state);
        }

        // ReSharper disable Unity.PerformanceAnalysis
        public void KillMainState()
        {
            if(_queue.Count > 0)
            {
                SetMainState(_queue.Dequeue());
            }
            else
            {
                AddDefaultState();
            }
        }

        private void Update()
        {
            MainState.Update();

            if(!MainState.IsAlive)
            {
                KillMainState();
            }
        }

        private void FixedUpdate()
        {
            if(MainState != null)
                MainState.FixedUpdate();
        }
    }
}