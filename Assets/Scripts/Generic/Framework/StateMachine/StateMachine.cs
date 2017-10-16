using System.Collections.Generic;

namespace DylanJay.Framework
{
    public class StateMachine
    {
        private IState _currentState = new NullState();
        public IState currentState { get { return _currentState; } private set { _currentState = value; } }

        private IState _previousState = new NullState();
        public IState previousState { get { return previousState; } private set { _previousState = value; } }

        public void ChangeState(IState newState)
        {
            if (currentState == newState)
            {
                return;
            }

            if (currentState == null)
            {
                previousState = newState;
            }
            else
            {
                currentState.Exit();
                previousState = currentState;
            }

            currentState = newState;
            currentState.Enter();
        }

        public void Update()
        {
            IState runningState = currentState;
            if (runningState != null)
            {
                runningState.Update();
            }
        }

        public void RevertToPreviousState()
        {
            if (previousState == null || currentState == null)
            {
                return;
            }
            
            currentState.Exit();
            currentState = previousState;
            currentState.Enter();
        }
	}
}