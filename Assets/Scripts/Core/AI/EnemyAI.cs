using UnityEngine;
using VContainer;
using FTL.Core.Services;
using FTL.Core.Components;

namespace FTL.Core.AI
{
    public class EnemyAI : MonoBehaviour
    {
        [Inject] public IWeaponService WeaponService { get; private set; }
        [Inject] public ICombatService CombatService { get; private set; }

        private WeaponComponent enemyWeapon;
        private float lastFireTime;
        private float fireInterval = 3f;

        public void SetWeapon(WeaponComponent weapon)
        {
            enemyWeapon = weapon;
        }

        private void Update()
        {
            if (!CombatService.IsCombatActive()) return;
            if (enemyWeapon == null) return;

            // Simple AI: fire weapon every few seconds
            if (Time.time - lastFireTime >= fireInterval)
            {
                if (WeaponService.IsWeaponReady(enemyWeapon))
                {
                    // Target player ship
                    var playerShip = CombatService.GetPlayerShip();
                    if (playerShip != null)
                    {
                        Vector3 targetPosition = playerShip.transform.position;
                        WeaponService.FireWeapon(enemyWeapon, targetPosition);
                        lastFireTime = Time.time;
                    }
                }
            }
        }
    }
}