using System;
using System.Collections.Generic;
using UnityEngine;

namespace GameStates
{
    public class GameStateManager
    {
        public event Action<GameState> GameStateChangedEvent;

        public GameState CurrentState;

        private Stack<GameState> _states;

        public GameStateManager()
        {
            _states = new();
        }

        public void PushState(GameState state)
        {
            if(CurrentState == null && _states.Count <= 0)
            {
                SetState(state);
            }
            else
            {
                _states.Push(state);
            }

            //  debugging
            string val = "STATES: ";
            foreach(var st in _states)
                val += $"{st.GetType().Name} < ";
            Debug.Log(val);
            //  ------------------------------
        }

        public void ClearStack()
        {
            _states.Clear();
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
        }

        void SetDefaultState()
        {
            SetState(new GameStateMain());
        }

        public void NextState()
        {
            if(_states.Count > 0)
            {
                SetState(_states.Pop());
            }
            else
            {
                SetDefaultState();
            }
        }

        public void Update()
        {
            if(Input.GetKeyDown(KeyCode.P))
            {
                PushState(new GameStateGameOver());
                CurrentState.Exit();
            }

            if(CurrentState == null)
            {
                NextState();
            }

            if(!CurrentState.IsAlive)
            {
                NextState();
            }
            else
            {
                CurrentState.Update();
                if(!CurrentState.IsAlive)
                    NextState();
            }
        }

        public void FixedUpdate()
        {
            CurrentState?.FixedUpdate();
        }
    }
}