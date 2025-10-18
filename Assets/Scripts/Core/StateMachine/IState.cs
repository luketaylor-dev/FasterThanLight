using UnityEngine;

namespace FTL.Core.StateMachine
{
    /// <summary>
    /// Base interface for all game states.
    /// Defines the contract that every state must implement.
    /// </summary>
    /// <typeparam name="T">The type of context object that uses this state</typeparam>
    public interface IState<T> where T : class
    {
        void OnEnter();
        
        void OnUpdate();
        
        void OnExit();
        
        string StateName { get; }
    }
}
