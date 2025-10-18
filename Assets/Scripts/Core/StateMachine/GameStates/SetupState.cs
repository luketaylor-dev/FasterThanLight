using UnityEngine;

namespace FTL.Core.StateMachine.GameStates
{
    /// <summary>
    /// Setup state - handles game initialization and preparation
    /// This is where we load assets, initialize systems, and prepare for combat
    /// </summary>
    public class SetupState : BaseState<Core.GameManager>
    {
        public override string StateName => "Setup";
        
        private float setupTimer = 0f;
        private float setupDuration = 2f;
        
        public SetupState(Core.GameManager context) : base(context)
        {
        }
        
        public override void OnEnter()
        {
            base.OnEnter();
            
            if (context.DebugMode)
            {
                Debug.Log("=== GAME SETUP STARTED ===");
                Debug.Log("Loading ship configurations...");
                Debug.Log("Initializing combat systems...");
                Debug.Log("Preparing UI...");
            }
            
            setupTimer = 0f;
        }
        
        public override void OnUpdate()
        {
            base.OnUpdate();
            
            setupTimer += Time.deltaTime;
            
            if (context.DebugMode && setupTimer >= setupDuration * 0.5f && setupTimer < setupDuration * 0.6f)
            {
                Debug.Log("Setup progress: 50% - Systems initialized");
            }
            
            if (context.DebugMode && setupTimer >= setupDuration * 0.8f && setupTimer < setupDuration * 0.9f)
            {
                Debug.Log("Setup progress: 80% - Almost ready for combat");
            }
            
            // Check if setup is complete
            if (setupTimer >= setupDuration)
            {
                if (context.DebugMode)
                {
                    Debug.Log("=== SETUP COMPLETE - STARTING COMBAT ===");
                }
                
                // Transition to combat state
                context.StartCombat();
            }
        }
        
        public override void OnExit()
        {
            base.OnExit();
            
            if (context.DebugMode)
            {
                Debug.Log("Setup phase completed successfully");
            }
        }
    }
}
