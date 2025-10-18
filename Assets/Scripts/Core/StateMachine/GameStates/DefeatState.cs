using UnityEngine;

namespace FTL.Core.StateMachine.GameStates
{
    /// <summary>
    /// Defeat state - handles game defeat
    /// This is shown when the player's ship is destroyed
    /// </summary>
    public class DefeatState : BaseState<Core.GameManager>
    {
        public override string StateName => "Defeat";
        
        private float defeatTimer = 0f;
        private bool hasShownDefeatMessage = false;
        
        public DefeatState(Core.GameManager context) : base(context)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            defeatTimer = 0f;
            hasShownDefeatMessage = false;
            
            if (context.DebugMode)
            {
                Debug.Log("=== DEFEAT ===");
                Debug.Log("Your ship has been destroyed!");
                Debug.Log("Press R to restart or ESC to quit");
            }
            
            // TODO: Show defeat UI
            // - Defeat screen
            // - Final score display
            // - Restart/quit buttons
            
            Time.timeScale = 0f;
        }
        
        public override void OnUpdate()
        {
            base.OnUpdate();
            
            defeatTimer += Time.unscaledDeltaTime;
            
            if (!hasShownDefeatMessage && defeatTimer >= 1f)
            {
                hasShownDefeatMessage = true;
                if (context.DebugMode)
                {
                    Debug.Log("Mission failed! Better luck next time!");
                }
            }
            
            if (Input.GetKeyDown(KeyCode.R))
            {
                if (context.DebugMode)
                {
                    Debug.Log("Restart requested - returning to setup");
                }
                RestartGame();
            }
            
            if (Input.GetKeyDown(KeyCode.Escape))
            {
                if (context.DebugMode)
                {
                    Debug.Log("Quit requested");
                }
                QuitGame();
            }
        }
        
        public override void OnExit()
        {
            base.OnExit();
            
            if (context.DebugMode)
            {
                Debug.Log($"Defeat state ended after {defeatTimer:F1} seconds");
            }
            
            // TODO: Cleanup defeat UI
            // - Hide defeat screen
            // - Reset game state
            
            Time.timeScale = 1f;
        }

        private void RestartGame()
        {
            // TODO: Reset all game systems
            // - Reset ships
            // - Reset weapons
            // - Reset UI

            context.StateMachine.ChangeState<SetupState>();
        }
        
        private void QuitGame()
        {
            if (context.DebugMode)
            {
                Debug.Log("Quitting game...");
            }
            
            #if UNITY_EDITOR
                UnityEditor.EditorApplication.isPlaying = false;
            #else
                Application.Quit();
            #endif
        }
    }
}
