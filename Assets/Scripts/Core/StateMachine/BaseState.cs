using UnityEngine;

namespace FTL.Core.StateMachine
{
    /// <summary>
    /// Base class for all game states.
    /// Provides common functionality and makes state implementation easier.
    /// </summary>
    /// <typeparam name="T">The type of context object that uses this state</typeparam>
    public abstract class BaseState<T> : IState<T> where T : class
    {
        protected T context;
        
        public abstract string StateName { get; }
        
        public BaseState(T context)
        {
            this.context = context;
        }
        
        public virtual void OnEnter()
        {
            Debug.Log($"Entered state: {StateName}");
        }
        
        public virtual void OnUpdate()
        {
        }
        
        public virtual void OnExit()
        {
            Debug.Log($"Exited state: {StateName}");
        }
    }
}
