using System;
using System.Collections.Generic;
using UnityEngine;

namespace Common.StateMachine
{
    public abstract class StateMachine<T, S> where T : Enum where S : State<T>
    {
        public event Action<T> StateChanged; 
        
        protected Dictionary<T, S> states;
        protected S currentState;

        public S CurrentState => currentState;

        protected void InitializeStates()
        {
            foreach (var state in states)
            {
                state.Value.WantsToChangeState += OnStateWantsToChange;
            }
        }

        private void OnStateWantsToChange(T newStateType)
        {
            ChangeState(newStateType);
        }

        public void ChangeState(T type)
        {
            currentState?.OnExit();

            if (!states.TryGetValue(type, out var state))
            {
                Debug.LogError("State error: " + type);
                return;
            }

            currentState = state;
            currentState.OnEnter();
            
            StateChanged?.Invoke(type);
        }
        
        public void Update()
        {
            currentState?.Update();
        }
    }
}