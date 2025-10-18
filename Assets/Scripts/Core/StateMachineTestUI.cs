using UnityEngine;
using UnityEngine.UI;
using TMPro;

namespace FTL.Core
{
    /// <summary>
    /// Simple UI controller to display current game state and handle input
    /// This helps us test our state machine implementation
    /// </summary>
    public class StateMachineTestUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI currentStateText;
        [SerializeField] private TextMeshProUGUI instructionsText;
        [SerializeField] private Button pauseButton;
        [SerializeField] private Button restartButton;

        [SerializeField] private GameManager gameManager;

        private void Start()
        {
            if (gameManager == null)
            {
                Debug.LogError("GameManager not found in scene! Please add GameManager to the scene.");
                return;
            }

            if (pauseButton != null)
            {
                pauseButton.onClick.AddListener(() => gameManager.TogglePause());
            }

            if (restartButton != null)
            {
                restartButton.onClick.AddListener(() => RestartGame());
            }

            UpdateUI();
        }

        private void Update()
        {
            UpdateUI();
        }

        private void UpdateUI()
        {
            if (gameManager == null) return;

            if (currentStateText != null)
            {
                string stateName = gameManager.StateMachine.CurrentState?.StateName ?? "Unknown";
                currentStateText.text = $"Current State: {stateName}";
            }

            if (instructionsText != null)
            {
                string instructions = GetInstructionsForCurrentState();
                instructionsText.text = instructions;
            }
        }

        private string GetInstructionsForCurrentState()
        {
            if (gameManager == null) return "";

            var currentState = gameManager.StateMachine.CurrentState;
            if (currentState == null) return "";

            return currentState.StateName switch
            {
                "Setup" => "Loading game systems... Please wait.\nEvent system initializing...",
                "Combat" => "Press SPACE to pause\nCombat is active!\nEvents firing automatically...",
                "Paused" => "Press SPACE to resume\nPlan your next move!\nEvents paused...",
                "Victory" => "Press R to restart\nPress ESC to quit\nYou won!\nCombat events ended",
                "Defeat" => "Press R to restart\nPress ESC to quit\nYou lost!\nCombat events ended",
                _ => "Unknown state"
            };
        }

        private void RestartGame()
        {
            if (gameManager == null) return;

            Debug.Log("Restarting game...");
            gameManager.StateMachine.ChangeState<StateMachine.GameStates.SetupState>();
        }

        private void QuitGame()
        {
            Debug.Log("Quitting game...");

#if UNITY_EDITOR
            UnityEditor.EditorApplication.isPlaying = false;
#else
                Application.Quit();
#endif
        }
    }
}
