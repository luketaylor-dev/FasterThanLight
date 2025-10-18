using UnityEngine;
using FTL.Core.Events;

namespace FTL.Core.Services
{
    public class ShipService : IShipService
    {
        private const float DEFAULT_SHIP_HEALTH = 100f;

        public ShipComponent CreateShip(string shipName, Vector3 position)
        {
            var ship = new GameObject(shipName);
            ship.transform.position = position;

            var shipComponent = ship.AddComponent<ShipComponent>();
            shipComponent.Initialize(DEFAULT_SHIP_HEALTH);

            CombatEvents.OnShipSpawned(ship, shipName, position);
            return shipComponent;
        }

        public void DestroyShip(ShipComponent ship)
        {
            if (ship == null) return;

            CombatEvents.OnShipDestroyed(ship.gameObject, ship.gameObject.name, ship.transform.position);
            Object.Destroy(ship.gameObject);
        }

        public bool IsShipAlive(ShipComponent ship)
        {
            return ship?.IsAlive ?? false;
        }

        public float GetShipHealth(ShipComponent ship)
        {
            return ship?.CurrentHealth ?? 0f;
        }

        public void DamageShip(ShipComponent ship, float damage)
        {
            ship?.TakeDamage(damage);
        }

        public Vector3 GetShipPosition(ShipComponent ship)
        {
            return ship?.transform.position ?? Vector3.zero;
        }

        public void SetShipPosition(ShipComponent ship, Vector3 position)
        {
            if (ship != null)
            {
                ship.transform.position = position;
            }
        }
    }
}
