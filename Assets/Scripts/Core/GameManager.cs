using UnityEngine;
using FTL.Core.Events;

namespace FTL.Core
{
    /// <summary>
    /// Main game manager that orchestrates the entire game flow.
    /// This is the context object that will use our state machine.
    /// </summary>
    public class GameManager : MonoBehaviour
    {
        [Header("Game Settings")]
        [SerializeField] private bool debugMode = true;

        private StateMachine.StateMachine<GameManager> stateMachine;

        public StateMachine.StateMachine<GameManager> StateMachine => stateMachine;

        public bool DebugMode => debugMode;

        private void Awake()
        {
            CombatEvents.Initialize();

            stateMachine = new StateMachine.StateMachine<GameManager>(this);

            InitializeStates();

            stateMachine.ChangeState<StateMachine.GameStates.SetupState>();
        }

        private void Update()
        {
            stateMachine?.Update();
        }

        private void InitializeStates()
        {
            stateMachine.AddState(new StateMachine.GameStates.SetupState(this));
            stateMachine.AddState(new StateMachine.GameStates.CombatState(this));
            stateMachine.AddState(new StateMachine.GameStates.PausedState(this));
            stateMachine.AddState(new StateMachine.GameStates.VictoryState(this));
            stateMachine.AddState(new StateMachine.GameStates.DefeatState(this));
        }

        public void StartCombat()
        {
            if (stateMachine.IsInState<StateMachine.GameStates.SetupState>())
            {
                stateMachine.ChangeState<StateMachine.GameStates.CombatState>();
            }
        }

        public void TogglePause()
        {
            if (stateMachine.IsInState<StateMachine.GameStates.CombatState>())
            {
                stateMachine.ChangeState<StateMachine.GameStates.PausedState>();
            }
            else if (stateMachine.IsInState<StateMachine.GameStates.PausedState>())
            {
                stateMachine.ChangeState<StateMachine.GameStates.CombatState>();
            }
        }

        public void EndGameVictory()
        {
            stateMachine.ChangeState<StateMachine.GameStates.VictoryState>();
        }

        public void EndGameDefeat()
        {
            stateMachine.ChangeState<StateMachine.GameStates.DefeatState>();
        }
    }
}
