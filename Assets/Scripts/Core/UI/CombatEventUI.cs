using UnityEngine;
using UnityEngine.UI;
using TMPro;
using FTL.Core.Events;

namespace FTL.Core.UI
{
    /// <summary>
    /// UI component that listens to combat events and updates the display.
    /// This demonstrates the Observer pattern in action.
    /// </summary>
    public class CombatEventUI : MonoBehaviour
    {
        [Header("UI References")]
        [SerializeField] private TextMeshProUGUI eventLogText;
        [SerializeField] private TextMeshProUGUI combatStatusText;
        [SerializeField] private ScrollRect eventScrollRect;

        [Header("Settings")]
        [SerializeField] private int maxEventLogEntries = 10;
        [SerializeField] private bool showDebugEvents = true;

        private string eventLog = "";
        private int eventCount = 0;

        private void Start()
        {
            SubscribeToEvents();

            UpdateCombatStatus("Waiting for combat to start...");
        }

        private void OnDestroy()
        {
            UnsubscribeFromEvents();
        }

        private void SubscribeToEvents()
        {
            // Combat flow events
            CombatEvents.CombatStarted.AddListener(OnCombatStarted);
            CombatEvents.CombatEnded.AddListener(OnCombatEnded);
            CombatEvents.GamePaused.AddListener(OnGamePaused);
            CombatEvents.GameResumed.AddListener(OnGameResumed);

            // Ship events
            CombatEvents.ShipDestroyed.AddListener(OnShipDestroyed);
            CombatEvents.ShipSpawned.AddListener(OnShipSpawned);

            // Room events
            CombatEvents.RoomDamaged.AddListener(OnRoomDamaged);
            CombatEvents.RoomDestroyed.AddListener(OnRoomDestroyed);

            // Shield events
            CombatEvents.ShieldHit.AddListener(OnShieldHit);
            CombatEvents.ShieldDestroyed.AddListener(OnShieldDestroyed);

            // Weapon events
            CombatEvents.WeaponFired.AddListener(OnWeaponFired);
            CombatEvents.WeaponCharging.AddListener(OnWeaponCharging);
            CombatEvents.WeaponReady.AddListener(OnWeaponReady);
            CombatEvents.WeaponCooldown.AddListener(OnWeaponCooldown);

            // Targeting events
            CombatEvents.TargetSelected.AddListener(OnTargetSelected);
            CombatEvents.TargetCleared.AddListener(OnTargetCleared);
        }

        private void UnsubscribeFromEvents()
        {
            // Combat flow events
            CombatEvents.CombatStarted.RemoveListener(OnCombatStarted);
            CombatEvents.CombatEnded.RemoveListener(OnCombatEnded);
            CombatEvents.GamePaused.RemoveListener(OnGamePaused);
            CombatEvents.GameResumed.RemoveListener(OnGameResumed);

            // Ship events
            CombatEvents.ShipDestroyed.RemoveListener(OnShipDestroyed);
            CombatEvents.ShipSpawned.RemoveListener(OnShipSpawned);

            // Room events
            CombatEvents.RoomDamaged.RemoveListener(OnRoomDamaged);
            CombatEvents.RoomDestroyed.RemoveListener(OnRoomDestroyed);

            // Shield events
            CombatEvents.ShieldHit.RemoveListener(OnShieldHit);
            CombatEvents.ShieldDestroyed.RemoveListener(OnShieldDestroyed);

            // Weapon events
            CombatEvents.WeaponFired.RemoveListener(OnWeaponFired);
            CombatEvents.WeaponCharging.RemoveListener(OnWeaponCharging);
            CombatEvents.WeaponReady.RemoveListener(OnWeaponReady);
            CombatEvents.WeaponCooldown.RemoveListener(OnWeaponCooldown);

            // Targeting events
            CombatEvents.TargetSelected.RemoveListener(OnTargetSelected);
            CombatEvents.TargetCleared.RemoveListener(OnTargetCleared);
        }

        #region Event Handlers

        private void OnCombatStarted()
        {
            AddEventLog("Combat Started!");
            UpdateCombatStatus("Combat Active");
        }

        private void OnCombatEnded()
        {
            AddEventLog("Combat Ended!");
            UpdateCombatStatus("Combat Finished");
        }

        private void OnGamePaused()
        {
            AddEventLog("Game Paused");
            UpdateCombatStatus("Game Paused");
        }

        private void OnGameResumed()
        {
            AddEventLog("Game Resumed");
            UpdateCombatStatus("Combat Active");
        }

        private void OnShipDestroyed(ShipEventData eventData)
        {
            AddEventLog($"Ship Destroyed: {eventData.shipName}");
        }

        private void OnShipSpawned(ShipEventData eventData)
        {
            AddEventLog($"Ship Spawned: {eventData.shipName}");
        }

        private void OnRoomDamaged(RoomDamageEventData eventData)
        {
            AddEventLog($"Room Damaged: {eventData.roomType} (-{eventData.damageAmount:F1})");
        }

        private void OnRoomDestroyed(RoomDamageEventData eventData)
        {
            AddEventLog($"Room Destroyed: {eventData.roomType}");
        }

        private void OnShieldHit(ShieldHitEventData eventData)
        {
            AddEventLog($"Shield Hit! {eventData.remainingLayers} layers remaining");
        }

        private void OnShieldDestroyed(ShieldHitEventData eventData)
        {
            AddEventLog("Shields Down!");
        }

        private void OnWeaponFired(WeaponFiredEventData eventData)
        {
            AddEventLog($"{eventData.weaponType} Fired!");
        }

        private void OnWeaponCharging(WeaponFiredEventData eventData)
        {
            AddEventLog($"{eventData.weaponType} Charging...");
        }

        private void OnWeaponReady(WeaponFiredEventData eventData)
        {
            AddEventLog($"{eventData.weaponType} Ready!");
        }

        private void OnWeaponCooldown(WeaponFiredEventData eventData)
        {
            AddEventLog($"{eventData.weaponType} Cooldown");
        }

        private void OnTargetSelected(TargetSelectedEventData eventData)
        {
            AddEventLog($"Target Selected: {eventData.targetType}");
        }

        private void OnTargetCleared(TargetSelectedEventData eventData)
        {
            AddEventLog("Target Cleared");
        }

        #endregion

        private void AddEventLog(string message)
        {
            if (!showDebugEvents) return;

            eventCount++;
            string timestamp = System.DateTime.Now.ToString("HH:mm:ss");
            string logEntry = $"[{timestamp}] {message}";

            if (string.IsNullOrEmpty(eventLog))
            {
                eventLog = logEntry;
            }
            else
            {
                eventLog = logEntry + "\n" + eventLog;
            }

            string[] lines = eventLog.Split('\n');
            if (lines.Length > maxEventLogEntries)
            {
                System.Array.Resize(ref lines, maxEventLogEntries);
                eventLog = string.Join("\n", lines);
            }

            if (eventLogText != null)
            {
                eventLogText.text = eventLog;
            }

            if (eventScrollRect != null)
            {
                eventScrollRect.verticalNormalizedPosition = 1f;
            }
        }
        private void UpdateCombatStatus(string status)
        {
            if (combatStatusText != null)
            {
                combatStatusText.text = $"Status: {status}";
            }
        }

        public void ClearEventLog()
        {
            eventLog = "";
            eventCount = 0;

            if (eventLogText != null)
            {
                eventLogText.text = "";
            }
        }
    }
}
