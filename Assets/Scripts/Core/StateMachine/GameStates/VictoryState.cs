using UnityEngine;

namespace FTL.Core.StateMachine.GameStates
{
    /// <summary>
    /// Victory state - handles game victory
    /// This is shown when the player defeats the enemy ship
    /// </summary>
    public class VictoryState : BaseState<Core.GameManager>
    {
        public override string StateName => "Victory";
        
        private float victoryTimer = 0f;
        private bool hasShownVictoryMessage = false;
        
        public VictoryState(Core.GameManager context) : base(context)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            victoryTimer = 0f;
            hasShownVictoryMessage = false;
            
            if (context.DebugMode)
            {
                Debug.Log("=== VICTORY! ===");
                Debug.Log("Enemy ship destroyed!");
                Debug.Log("Press R to restart or ESC to quit");
            }
            
            // TODO: Show victory UI
            // - Victory screen
            // - Score display
            // - Restart/quit buttons
            
            Time.timeScale = 0f;
        }
        
        public override void OnUpdate()
        {
            base.OnUpdate();
            
            victoryTimer += Time.unscaledDeltaTime; 
            
            if (!hasShownVictoryMessage && victoryTimer >= 1f)
            {
                hasShownVictoryMessage = true;
                if (context.DebugMode)
                {
                    Debug.Log("Congratulations! You have achieved victory!");
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
                Debug.Log($"Victory state ended after {victoryTimer:F1} seconds");
            }
            
            // TODO: Cleanup victory UI
            // - Hide victory screen
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
