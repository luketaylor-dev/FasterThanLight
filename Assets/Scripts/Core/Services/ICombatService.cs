using UnityEngine;

namespace FTL.Core.Services
{
    public interface ICombatService
    {
        void StartCombat(ShipComponent playerShip, ShipComponent enemyShip);
        void EndCombat();
        bool IsCombatActive();
        ShipComponent GetPlayerShip();
        ShipComponent GetEnemyShip();
        void ProcessCombatTurn();
        bool CheckVictoryCondition();
        bool CheckDefeatCondition();
    }
}
