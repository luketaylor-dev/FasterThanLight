using UnityEngine;
using FTL.Core.Events;

namespace FTL.Core.Services
{
    public class WeaponService : IWeaponService
    {
        private const float DEFAULT_CHARGE_TIME = 2f;
        private const float DEFAULT_COOLDOWN_TIME = 1f;
        private const float DEFAULT_DAMAGE = 50f;

        public WeaponComponent CreateWeapon(WeaponType weaponType, ShipComponent ship, Vector3 position)
        {
            var weapon = new GameObject($"{weaponType}Weapon");
            weapon.transform.position = position;
            weapon.transform.SetParent(ship.transform);

            var weaponComponent = weapon.AddComponent<WeaponComponent>();
            weaponComponent.Initialize(weaponType, DEFAULT_DAMAGE, DEFAULT_CHARGE_TIME, DEFAULT_COOLDOWN_TIME);

            return weaponComponent;
        }

        public void DestroyWeapon(WeaponComponent weapon)
        {
            if (weapon == null) return;

            Object.Destroy(weapon.gameObject);
        }

        public bool IsWeaponReady(WeaponComponent weapon)
        {
            return weapon?.IsReady ?? false;
        }

        public void FireWeapon(WeaponComponent weapon, Vector3 targetPosition)
        {
            weapon?.Fire(targetPosition);
        }

        public void StartCharging(WeaponComponent weapon)
        {
            weapon?.StartCharging();
        }

        public float GetChargeProgress(WeaponComponent weapon)
        {
            return weapon?.ChargeProgress ?? 0f;
        }

        public float GetCooldownProgress(WeaponComponent weapon)
        {
            return weapon?.CooldownProgress ?? 0f;
        }

        public WeaponType GetWeaponType(WeaponComponent weapon)
        {
            return weapon?.WeaponType ?? WeaponType.LaserCannon;
        }

        public float GetWeaponDamage(WeaponComponent weapon)
        {
            return weapon?.Damage ?? 0f;
        }
    }
}
