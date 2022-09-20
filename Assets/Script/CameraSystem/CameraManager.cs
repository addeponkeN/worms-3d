using System.Collections.Generic;
using CameraSystem.CameraStates;
using Cinemachine;
using GameStates;
using UnityEngine;
using VoxelEngine;

namespace CameraSystem
{
    public class CameraManager : MonoBehaviour, ILoader
    {
        public bool LoadingComplete { get; set; }

        public Camera Cam;
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

            Cam = Camera.main;
            MoveCam = GameObject.Find("CM Movement").GetComponent<CinemachineVirtualCamera>();
            AimCam = GameObject.Find("CM Aiming").GetComponent<CinemachineVirtualCamera>();
        }

        private void Start()
        {
            AddDefaultState();
            World.Get.OnGeneratedEvent += OnWorldGeneratedEvent;
        }

        public void Load()
        {
            LoadingComplete = true;
        }

        private void OnWorldGeneratedEvent()
        {
        }

        public void SetMainState(ICameraState state)
        {
            if(MainState != null)
            {
                MainState.OnExit();
            }

            MainState = state;
            MainState.Camera = Cam;
            MainState.IsAlive = true;
            MainState.Manager = this;
            MainState.Init();
        }

        public void AddDefaultState()
        {
            // SetMainState(new FollowPlayerState(GameManager.Get.ControllerManager.CurrentPlayer));
        }

        public void QueueMainState(ICameraState state)
        {
            _queue.Enqueue(state);
        }

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
            if(MainState != null)
            {
                MainState.Update();

                if(!MainState.IsAlive)
                {
                    KillMainState();
                }
            }
        }

        private void FixedUpdate()
        {
            if(MainState != null)
                MainState.FixedUpdate();
        }
    }
}