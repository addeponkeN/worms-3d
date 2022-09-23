using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates
{
    public class GameStateManager
    {
        private Stack<GameState> States;

        public GameState CurrentState;

        public event Action<GameState> GameStateChangedEvent;

        public GameStateManager()
        {
            States = new();
        }

        public void PushState(GameState state)
        {
            if(CurrentState == null && States.Count <= 0)
            {
                SetState(state);
            }
            else
            {
                States.Push(state);
            }

            Debug.Log($"STATE PUSH: {state.GetType().Name}");
        }

        public void ClearStack()
        {
            States.Clear();
            SetDefaultState();
        }

        public void ForceSetState(GameState state)
        {
            SetState(state);
        }
        
        void SetState(GameState state)
        {
            //  if current state is (not null & is alive), exit it
            if(CurrentState is {IsAlive: true})
            {
                CurrentState.Exit();
            }
            
            CurrentState = state;
            CurrentState.Init(this);
            GameStateChangedEvent?.Invoke(CurrentState);
            // Debug.Log($"STATE: {state.GetType().Name}");
        }

        void SetDefaultState()
        {
            SetState(new GameStateMain());
        }

        public void NextState()
        {
            if(States.Count > 0)
            {
                SetState(States.Pop());
            }
            else
            {
                SetDefaultState();
            }
        }

        public void Update()
        {
            if(CurrentState == null)
            {
                NextState();
            }

            if(CurrentState != null)
            {
                CurrentState.Update();
                if(!CurrentState.IsAlive)
                {
                    NextState();
                }
            }
        }

        public void FixedUpdate()
        {
            if(CurrentState != null)
            {
                CurrentState.FixedUpdate();
            }
        }
        
    }
}