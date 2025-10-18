using UnityEngine;
using UnityEngine.Events;

namespace FTL.Core.Events
{
    /// <summary>
    /// Central event system for combat interactions using the Observer pattern.
    /// This class provides a decoupled way for systems to communicate without direct references.
    /// </summary>
    public static class CombatEvents
    {
        // Ship Events
        public static UnityEvent<ShipEventData> ShipDestroyed { get; private set; } = new UnityEvent<ShipEventData>();
        public static UnityEvent<ShipEventData> ShipSpawned { get; private set; } = new UnityEvent<ShipEventData>();

        // Room Events
        public static UnityEvent<RoomDamageEventData> RoomDamaged { get; private set; } = new UnityEvent<RoomDamageEventData>();
        public static UnityEvent<RoomDamageEventData> RoomDestroyed { get; private set; } = new UnityEvent<RoomDamageEventData>();
        public static UnityEvent<RoomDamageEventData> RoomRepaired { get; private set; } = new UnityEvent<RoomDamageEventData>();

        // Shield Events
        public static UnityEvent<ShieldHitEventData> ShieldHit { get; private set; } = new UnityEvent<ShieldHitEventData>();
        public static UnityEvent<ShieldHitEventData> ShieldDestroyed { get; private set; } = new UnityEvent<ShieldHitEventData>();
        public static UnityEvent<ShieldHitEventData> ShieldRecharged { get; private set; } = new UnityEvent<ShieldHitEventData>();

        // Weapon Events
        public static UnityEvent<WeaponFiredEventData> WeaponFired { get; private set; } = new UnityEvent<WeaponFiredEventData>();
        public static UnityEvent<WeaponFiredEventData> WeaponCharging { get; private set; } = new UnityEvent<WeaponFiredEventData>();
        public static UnityEvent<WeaponFiredEventData> WeaponReady { get; private set; } = new UnityEvent<WeaponFiredEventData>();
        public static UnityEvent<WeaponFiredEventData> WeaponCooldown { get; private set; } = new UnityEvent<WeaponFiredEventData>();

        // Targeting Events
        public static UnityEvent<TargetSelectedEventData> TargetSelected { get; private set; } = new UnityEvent<TargetSelectedEventData>();
        public static UnityEvent<TargetSelectedEventData> TargetCleared { get; private set; } = new UnityEvent<TargetSelectedEventData>();

        // Combat Flow Events
        public static UnityEvent CombatStarted { get; private set; } = new UnityEvent();
        public static UnityEvent CombatEnded { get; private set; } = new UnityEvent();
        public static UnityEvent GamePaused { get; private set; } = new UnityEvent();
        public static UnityEvent GameResumed { get; private set; } = new UnityEvent();

        public static void Initialize()
        {
            ShipDestroyed = new UnityEvent<ShipEventData>();
            ShipSpawned = new UnityEvent<ShipEventData>();

            RoomDamaged = new UnityEvent<RoomDamageEventData>();
            RoomDestroyed = new UnityEvent<RoomDamageEventData>();
            RoomRepaired = new UnityEvent<RoomDamageEventData>();

            ShieldHit = new UnityEvent<ShieldHitEventData>();
            ShieldDestroyed = new UnityEvent<ShieldHitEventData>();
            ShieldRecharged = new UnityEvent<ShieldHitEventData>();

            WeaponFired = new UnityEvent<WeaponFiredEventData>();
            WeaponCharging = new UnityEvent<WeaponFiredEventData>();
            WeaponReady = new UnityEvent<WeaponFiredEventData>();
            WeaponCooldown = new UnityEvent<WeaponFiredEventData>();

            TargetSelected = new UnityEvent<TargetSelectedEventData>();
            TargetCleared = new UnityEvent<TargetSelectedEventData>();

            CombatStarted = new UnityEvent();
            CombatEnded = new UnityEvent();
            GamePaused = new UnityEvent();
            GameResumed = new UnityEvent();

            Debug.Log("CombatEvents initialized successfully");
        }

        public static void ClearAllListeners()
        {
            ShipDestroyed.RemoveAllListeners();
            ShipSpawned.RemoveAllListeners();

            RoomDamaged.RemoveAllListeners();
            RoomDestroyed.RemoveAllListeners();
            RoomRepaired.RemoveAllListeners();

            ShieldHit.RemoveAllListeners();
            ShieldDestroyed.RemoveAllListeners();
            ShieldRecharged.RemoveAllListeners();

            WeaponFired.RemoveAllListeners();
            WeaponCharging.RemoveAllListeners();
            WeaponReady.RemoveAllListeners();
            WeaponCooldown.RemoveAllListeners();

            TargetSelected.RemoveAllListeners();
            TargetCleared.RemoveAllListeners();

            CombatStarted.RemoveAllListeners();
            CombatEnded.RemoveAllListeners();
            GamePaused.RemoveAllListeners();
            GameResumed.RemoveAllListeners();

            Debug.Log("All CombatEvents listeners cleared");
        }

        #region Event Invocation Methods

        public static void OnShipDestroyed(GameObject ship, string shipName, Vector3 position)
        {
            var eventData = new ShipEventData(ship, shipName, position);
            ShipDestroyed?.Invoke(eventData);
            Debug.Log($"Event: ShipDestroyed - {shipName} at {position}");
        }

        public static void OnShipSpawned(GameObject ship, string shipName, Vector3 position)
        {
            var eventData = new ShipEventData(ship, shipName, position);
            ShipSpawned?.Invoke(eventData);
            Debug.Log($"Event: ShipSpawned - {shipName} at {position}");
        }

        public static void OnRoomDamaged(GameObject room, GameObject ship, RoomType roomType,
            float damageAmount, float newHealth, Vector3 hitPosition)
        {
            var eventData = new RoomDamageEventData(room, ship, roomType, damageAmount, newHealth, hitPosition);
            RoomDamaged?.Invoke(eventData);
            Debug.Log($"Event: RoomDamaged - {roomType} took {damageAmount} damage, health: {newHealth}");
        }

        public static void OnRoomDestroyed(GameObject room, GameObject ship, RoomType roomType, Vector3 position)
        {
            var eventData = new RoomDamageEventData(room, ship, roomType, 0f, 0f, position);
            RoomDestroyed?.Invoke(eventData);
            Debug.Log($"Event: RoomDestroyed - {roomType}");
        }

        public static void OnShieldHit(GameObject ship, int shieldLayer, int remainingLayers,
            Vector3 hitPosition, bool shieldDestroyed)
        {
            var eventData = new ShieldHitEventData(ship, shieldLayer, remainingLayers, hitPosition, shieldDestroyed);
            ShieldHit?.Invoke(eventData);
            Debug.Log($"Event: ShieldHit - Layer {shieldLayer}, {remainingLayers} remaining");
        }

        public static void OnWeaponFired(GameObject weapon, GameObject ship, WeaponType weaponType,
            Vector3 firePosition, Vector3 targetPosition, float damage)
        {
            var eventData = new WeaponFiredEventData(weapon, ship, weaponType, firePosition, targetPosition, damage);
            WeaponFired?.Invoke(eventData);
            Debug.Log($"Event: WeaponFired - {weaponType} fired at {targetPosition}");
        }

        public static void OnWeaponCharging(GameObject weapon, GameObject ship, WeaponType weaponType,
            Vector3 firePosition, Vector3 targetPosition, float damage)
        {
            var eventData = new WeaponFiredEventData(weapon, ship, weaponType, firePosition, targetPosition, damage);
            WeaponCharging?.Invoke(eventData);
            Debug.Log($"Event: WeaponCharging - {weaponType} started charging");
        }

        public static void OnWeaponReady(GameObject weapon, GameObject ship, WeaponType weaponType,
            Vector3 firePosition, Vector3 targetPosition, float damage)
        {
            var eventData = new WeaponFiredEventData(weapon, ship, weaponType, firePosition, targetPosition, damage);
            WeaponReady?.Invoke(eventData);
            Debug.Log($"Event: WeaponReady - {weaponType} ready to fire");
        }

        public static void OnWeaponCooldown(GameObject weapon, GameObject ship, WeaponType weaponType,
            Vector3 firePosition, Vector3 targetPosition, float damage)
        {
            var eventData = new WeaponFiredEventData(weapon, ship, weaponType, firePosition, targetPosition, damage);
            WeaponCooldown?.Invoke(eventData);
            Debug.Log($"Event: WeaponCooldown - {weaponType} in cooldown");
        }

        public static void OnTargetSelected(GameObject target, GameObject ship, TargetType targetType, bool isValidTarget)
        {
            var eventData = new TargetSelectedEventData(target, ship, targetType, isValidTarget);
            TargetSelected?.Invoke(eventData);
            Debug.Log($"Event: TargetSelected - {targetType} targeting {target.name}");
        }

        public static void OnCombatStarted()
        {
            CombatStarted?.Invoke();
            Debug.Log("Event: CombatStarted");
        }
        public static void OnCombatEnded()
        {
            CombatEnded?.Invoke();
            Debug.Log("Event: CombatEnded");
        }

        public static void OnGamePaused()
        {
            GamePaused?.Invoke();
            Debug.Log("Event: GamePaused");
        }
        public static void OnGameResumed()
        {
            GameResumed?.Invoke();
            Debug.Log("Event: GameResumed");
        }

        #endregion
    }
}
