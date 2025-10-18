using UnityEngine;

namespace FTL.Core.Services
{
    public interface IShipService
    {
        ShipComponent CreateShip(string shipName, Vector3 position);
        (ShipComponent ship, AI.EnemyAI ai) CreateEnemyShip(string shipName, Vector3 position);
        void DestroyShip(ShipComponent ship);
        bool IsShipAlive(ShipComponent ship);
        float GetShipHealth(ShipComponent ship);
        void DamageShip(ShipComponent ship, float damage);
        Vector3 GetShipPosition(ShipComponent ship);
        void SetShipPosition(ShipComponent ship, Vector3 position);
    }
}
