using System;

namespace Common.StateMachine
{
    public abstract class State<T> where T : Enum
    {
        public event Action<T> WantsToChangeState;

        public abstract T Type { get; }
        
        public virtual void OnEnter() { }
        
        public virtual void Update() { }
        
        public virtual void OnExit() { }

        protected void RaiseChangeState(T newStateType)
        {
            WantsToChangeState?.Invoke(newStateType);
        }
    }
}