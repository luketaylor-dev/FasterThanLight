using UnityEngine;
using VContainer;
using FTL.Core.Events;
using FTL.Core.Services;
using FTL.Core.Components;

namespace FTL.Core.StateMachine.GameStates
{
    public class CombatState : BaseState<Core.GameManager>
    {
        public override string StateName => "Combat";

        [Inject] public IShipService ShipService { get; private set; }
        [Inject] public IWeaponService WeaponService { get; private set; }
        [Inject] public ICombatService CombatService { get; private set; }
        [Inject] public IAIService AIService { get; private set; }

        private float combatTimer = 0f;
        private ShipComponent playerShip;
        private ShipComponent enemyShip;
        private WeaponComponent playerWeapon;
        private WeaponComponent enemyWeapon;
        private AI.EnemyAI enemyAI;

        public CombatState(Core.GameManager context) : base(context)
        {
        }

        public override void OnEnter()
        {
            base.OnEnter();

            if (context.DebugMode)
            {
                Debug.Log("=== COMBAT STARTED ===");
                Debug.Log("Ships are now engaging in combat!");
                Debug.Log("Press SPACE to pause/unpause");
            }

            combatTimer = 0f;

            // Create ships using injected services
            playerShip = ShipService.CreateShip("Player Ship", Vector3.left);
            (enemyShip, enemyAI) = ShipService.CreateEnemyShip("Enemy Ship", Vector3.right);

            // Create weapons
            playerWeapon = WeaponService.CreateWeapon(WeaponType.LaserCannon, playerShip, Vector3.up);
            enemyWeapon = WeaponService.CreateWeapon(WeaponType.LaserCannon, enemyShip, Vector3.up);

            // Setup AI
            AIService.AssignWeaponToAI(enemyAI, enemyWeapon);
            AIService.StartAI(enemyAI);

            // Start combat
            CombatService.StartCombat(playerShip, enemyShip);

            CombatEvents.OnCombatStarted();
        }

        public override void OnUpdate()
        {
            base.OnUpdate();

            combatTimer += Time.deltaTime;

            if (Input.GetKeyDown(KeyCode.Space))
            {
                if (context.DebugMode)
                {
                    Debug.Log("Pause requested - transitioning to Paused state");
                }
                context.TogglePause();
                return;
            }

            // Process combat using injected services
            CombatService.ProcessCombatTurn();

            // Check win/lose conditions
            if (CombatService.CheckVictoryCondition())
            {
                if (context.DebugMode)
                {
                    Debug.Log("Victory condition met!");
                }
                context.EndGameVictory();
            }
            else if (CombatService.CheckDefeatCondition())
            {
                if (context.DebugMode)
                {
                    Debug.Log("Defeat condition met!");
                }
                context.EndGameDefeat();
            }

            if (context.DebugMode && combatTimer > 0f && combatTimer < 0.1f)
            {
                Debug.Log("Combat systems active - weapons charging...");
            }
        }

        public override void OnExit()
        {
            base.OnExit();

            if (context.DebugMode)
            {
                Debug.Log($"Combat phase ended after {combatTimer:F1} seconds");
            }

            // Cleanup using injected services
            CombatService.EndCombat();

            if (enemyAI != null) AIService.StopAI(enemyAI);
            if (playerWeapon != null) WeaponService.DestroyWeapon(playerWeapon);
            if (enemyWeapon != null) WeaponService.DestroyWeapon(enemyWeapon);
            if (playerShip != null) ShipService.DestroyShip(playerShip);
            if (enemyShip != null) ShipService.DestroyShip(enemyShip);

            CombatEvents.OnCombatEnded();
        }
    }
}
