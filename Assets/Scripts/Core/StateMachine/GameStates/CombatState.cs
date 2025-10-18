using UnityEngine;
using FTL.Core.Events;

namespace FTL.Core.StateMachine.GameStates
{
    /// <summary>
    /// Combat state - handles active gameplay
    /// This is where ships fight, weapons fire, and the main game loop runs
    /// </summary>
    public class CombatState : BaseState<Core.GameManager>
    {
        public override string StateName => "Combat";

        private float combatTimer = 0f;

        public CombatState(Core.GameManager context) : base(context)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (context.DebugMode)
            {
                Debug.Log("=== COMBAT STARTED ===");
                Debug.Log("Ships are now engaging in combat!");
                Debug.Log("Press SPACE to pause/unpause");
            }

            combatTimer = 0f;

            CombatEvents.OnCombatStarted();

            // TODO: Initialize combat systems here
            // - Spawn ships
            // - Initialize weapons
            // - Start AI
            // - Enable player input
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            combatTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (context.DebugMode)
                {
                    Debug.Log("Pause requested - transitioning to Paused state");
                }
                context.TogglePause();
                return;
            }

            // TODO: Update combat systems here
            // - Update weapons
            // - Update AI
            // - Check win/lose conditions
            // - Update UI

            if (context.DebugMode && combatTimer > 0f && combatTimer < 0.1f)
            {
                Debug.Log("Combat systems active - weapons charging...");
            }

            // TODO: Check win/lose conditions
            if (combatTimer >= 10f)
            {
                if (context.DebugMode)
                {
                    Debug.Log("Simulated victory condition met!");
                }
                context.EndGameVictory();
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            if (context.DebugMode)
            {
                Debug.Log($"Combat phase ended after {combatTimer:F1} seconds");
            }

            CombatEvents.OnCombatEnded();

            // TODO: Cleanup combat systems
            // - Stop weapons
            // - Disable AI
            // - Disable player input
        }
    }
}
