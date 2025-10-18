using UnityEngine;
using FTL.Core.Events;

namespace FTL.Core.Services
{
    public interface IWeaponService
    {
        WeaponComponent CreateWeapon(WeaponType weaponType, ShipComponent ship, Vector3 position);
        void DestroyWeapon(WeaponComponent weapon);
        bool IsWeaponReady(WeaponComponent weapon);
        void FireWeapon(WeaponComponent weapon, Vector3 targetPosition);
        void StartCharging(WeaponComponent weapon);
        float GetChargeProgress(WeaponComponent weapon);
        float GetCooldownProgress(WeaponComponent weapon);
        WeaponType GetWeaponType(WeaponComponent weapon);
        float GetWeaponDamage(WeaponComponent weapon);
    }
}
