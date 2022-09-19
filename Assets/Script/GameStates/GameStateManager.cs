using System.Collections.Generic;

namespace GameStates
{
    public class GameStateManager
    {
        private Stack<GameState> States;

        public GameState CurrentState;

        public GameStateManager()
        {
            States = new();
        }

        public void PushState(GameState state)
        {
            States.Push(state);
            state.Init(this);
        }

        public void ExitCurrentState()
        {
            States.Pop();
        }

        public void Update()
        {
            if(CurrentState != null)
            {
                CurrentState.Update();
                if(!CurrentState.IsAlive)
                {
                    ExitCurrentState();
                }
            }
        }

        public void FixedUpdate()
        {
            if(CurrentState != null)
            {
                CurrentState.Update();
            }
        }
    }
}