using UnityEngine;

namespace FTL.Core.StateMachine.GameStates
{
    /// <summary>
    /// Paused state - handles game pause functionality
    /// This allows players to plan their moves tactically
    /// </summary>
    public class PausedState : BaseState<Core.GameManager>
    {
        public override string StateName => "Paused";

        private float pauseStartTime;

        public PausedState(Core.GameManager context) : base(context)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            pauseStartTime = Time.time;

            if (context.DebugMode)
            {
                Debug.Log("=== GAME PAUSED ===");
                Debug.Log("Time to plan your next move!");
                Debug.Log("Press SPACE to resume combat");
            }

            // TODO: Pause combat systems
            // - Pause weapons
            // - Pause AI
            // - Show pause UI
            // - Allow tactical planning

            Time.timeScale = 0f;
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (context.DebugMode)
                {
                    Debug.Log("Unpause requested - resuming combat");
                }
                context.TogglePause();
                return;
            }

            // TODO: Handle pause-specific input
            // - Allow targeting while paused
            // - Show tactical information
            // - Allow system management
        }

        public override void OnExit()
        {
            base.OnExit();

            float pauseDuration = Time.time - pauseStartTime;

            if (context.DebugMode)
            {
                Debug.Log($"Game unpaused after {pauseDuration:F1} seconds");
            }

            // TODO: Resume combat systems
            // - Resume weapons
            // - Resume AI
            // - Hide pause UI

            Time.timeScale = 1f;
        }
    }
}
