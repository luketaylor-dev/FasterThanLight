using UnityEngine;
using System.Collections.Generic;

namespace FTL.Core.StateMachine
{
    /// <summary>
    /// Generic state machine that manages state transitions and updates.
    /// This is the core of our state management system.
    /// </summary>
    /// <typeparam name="T">The type of context object that uses this state machine</typeparam>
    public class StateMachine<T> where T : class
    {
        private IState<T> currentState;
        private T context;
        private Dictionary<System.Type, IState<T>> states;
        
        public IState<T> CurrentState => currentState;
        
        public T Context => context;
        
        public StateMachine(T context)
        {
            this.context = context;
            this.states = new Dictionary<System.Type, IState<T>>();
        }
        
        public void AddState(IState<T> state)
        {
            states[state.GetType()] = state;
        }
        
        public void ChangeState<TState>() where TState : class, IState<T>
        {
            // Exit current state
            if (currentState != null)
            {
                Debug.Log($"Exiting state: {currentState.StateName}");
                currentState.OnExit();
            }
            
            // Find and enter new state
            if (states.TryGetValue(typeof(TState), out IState<T> newState))
            {
                currentState = newState;
                Debug.Log($"Entering state: {currentState.StateName}");
                currentState.OnEnter();
            }
            else
            {
                Debug.LogError($"State {typeof(TState).Name} not found in state machine!");
            }
        }
        
        public void Update()
        {
            currentState?.OnUpdate();
        }
        
        public bool IsInState<TState>() where TState : class, IState<T>
        {
            return currentState is TState;
        }
    }
}
