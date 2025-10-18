using UnityEngine;
using FTL.Core.Components;
using FTL.Core.Events;

namespace FTL.Core.Services
{
    public class CombatService : ICombatService
    {
        private ShipComponent playerShip;
        private ShipComponent enemyShip;
        private bool combatActive;

        public void StartCombat(ShipComponent playerShip, ShipComponent enemyShip)
        {
            this.playerShip = playerShip;
            this.enemyShip = enemyShip;
            combatActive = true;

            CombatEvents.OnCombatStarted();
        }

        public void EndCombat()
        {
            combatActive = false;
            CombatEvents.OnCombatEnded();
        }

        public bool IsCombatActive()
        {
            return combatActive;
        }

        public ShipComponent GetPlayerShip()
        {
            return playerShip;
        }

        public ShipComponent GetEnemyShip()
        {
            return enemyShip;
        }

        public void ProcessCombatTurn()
        {
            if (!combatActive) return;

            if (CheckVictoryCondition())
            {
                EndCombat();
            }
            else if (CheckDefeatCondition())
            {
                EndCombat();
            }
        }

        public bool CheckVictoryCondition()
        {
            return enemyShip != null && !enemyShip.IsAlive;
        }

        public bool CheckDefeatCondition()
        {
            return playerShip != null && !playerShip.IsAlive;
        }
    }
}
