using UnityEngine;
using FTL.Core.Events;

namespace FTL.Core
{
    /// <summary>
    /// Test script to demonstrate the event system working.
    /// This simulates combat events for testing purposes.
    /// </summary>
    public class EventSystemTester : MonoBehaviour
    {
        [Header("Test Settings")]
        [SerializeField] private bool enableTesting = true;
        [SerializeField] private float testInterval = 2f;
        [SerializeField] private bool simulateCombatEvents = true;
        [SerializeField] private int maxEventsPerSecond = 5;

        private float testTimer = 0f;
        private int testEventCounter = 0;
        private int eventsThisSecond = 0;
        private float lastSecondTime = 0f;

        private GameObject dummyShip;
        private GameObject dummyRoom;
        private GameObject dummyWeapon;

        private void Awake()
        {
            dummyShip = new GameObject("TestShip");
            dummyRoom = new GameObject("TestRoom");
            dummyWeapon = new GameObject("TestWeapon");

            dummyShip.hideFlags = HideFlags.HideInHierarchy;
            dummyRoom.hideFlags = HideFlags.HideInHierarchy;
            dummyWeapon.hideFlags = HideFlags.HideInHierarchy;
        }

        private void OnDestroy()
        {
            if (dummyShip != null) DestroyImmediate(dummyShip);
            if (dummyRoom != null) DestroyImmediate(dummyRoom);
            if (dummyWeapon != null) DestroyImmediate(dummyWeapon);
        }

        private void Update()
        {
            if (!enableTesting || !simulateCombatEvents) return;

            if (Time.time - lastSecondTime >= 1f)
            {
                eventsThisSecond = 0;
                lastSecondTime = Time.time;
            }

            if (eventsThisSecond >= maxEventsPerSecond) return;

            testTimer += Time.deltaTime;

            if (testTimer >= testInterval)
            {
                testTimer = 0f;
                SimulateRandomEvent();
                eventsThisSecond++;
            }
        }

        private void SimulateRandomEvent()
        {
            testEventCounter++;

            int eventType = Random.Range(0, 6);

            switch (eventType)
            {
                case 0:
                    CombatEvents.OnShipSpawned(dummyShip, $"Enemy Ship {testEventCounter}", Vector3.zero);
                    break;

                case 1:
                    CombatEvents.OnRoomDamaged(dummyRoom, dummyShip, RoomType.EmptyRoom,
                        Random.Range(10f, 50f), Random.Range(20f, 100f), Vector3.zero);
                    break;

                case 2:
                    CombatEvents.OnShieldHit(dummyShip, Random.Range(1, 4), Random.Range(0, 3),
                        Vector3.zero, Random.value > 0.7f);
                    break;

                case 3:
                    CombatEvents.OnWeaponFired(dummyWeapon, dummyShip, WeaponType.LaserCannon,
                        Vector3.zero, Vector3.right, Random.Range(25f, 75f));
                    break;

                case 4:
                    CombatEvents.OnTargetSelected(dummyRoom, dummyShip, TargetType.Room, true);
                    break;

                case 5:
                    CombatEvents.OnRoomDestroyed(dummyRoom, dummyShip, RoomType.EmptyRoom, Vector3.zero);
                    break;
            }
        }

        [ContextMenu("Test Ship Spawned")]
        public void TestShipSpawned()
        {
            CombatEvents.OnShipSpawned(dummyShip, "Test Ship", Vector3.zero);
        }

        [ContextMenu("Test Room Damaged")]
        public void TestRoomDamaged()
        {
            CombatEvents.OnRoomDamaged(dummyRoom, dummyShip, RoomType.EmptyRoom, 25f, 75f, Vector3.zero);
        }

        [ContextMenu("Test Shield Hit")]
        public void TestShieldHit()
        {
            CombatEvents.OnShieldHit(dummyShip, 2, 1, Vector3.zero, false);
        }

        [ContextMenu("Test Weapon Fired")]
        public void TestWeaponFired()
        {
            CombatEvents.OnWeaponFired(dummyWeapon, dummyShip, WeaponType.LaserCannon, Vector3.zero, Vector3.right, 50f);
        }

        [ContextMenu("Test Target Selected")]
        public void TestTargetSelected()
        {
            CombatEvents.OnTargetSelected(dummyRoom, dummyShip, TargetType.Room, true);
        }

        [ContextMenu("Toggle Event Simulation")]
        public void ToggleEventSimulation()
        {
            simulateCombatEvents = !simulateCombatEvents;
            Debug.Log($"Event simulation {(simulateCombatEvents ? "enabled" : "disabled")}");
        }
    }
}
