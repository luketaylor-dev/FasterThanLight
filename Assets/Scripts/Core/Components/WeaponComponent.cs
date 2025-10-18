using UnityEngine;
using FTL.Core.Events;

namespace FTL.Core.Components
{
    public class WeaponComponent : MonoBehaviour
    {
        public WeaponType WeaponType { get; private set; }
        public float Damage { get; private set; }
        public float ChargeProgress { get; private set; }
        public float CooldownProgress { get; private set; }
        public bool IsReady { get; private set; }

        private float chargeTime;
        private float cooldownTime;
        private bool isCharging;
        private bool isOnCooldown;

        public void Initialize(WeaponType type, float weaponDamage, float chargeDuration, float cooldownDuration)
        {
            WeaponType = type;
            Damage = weaponDamage;
            chargeTime = chargeDuration;
            cooldownTime = cooldownDuration;

            ResetState();
        }

        private void Update()
        {
            if (isCharging)
            {
                ChargeProgress += Time.deltaTime / chargeTime;
                if (ChargeProgress >= 1f)
                {
                    ChargeProgress = 1f;
                    isCharging = false;
                    IsReady = true;
                    CombatEvents.OnWeaponReady(gameObject, transform.parent.gameObject, WeaponType,
                        transform.position, Vector3.zero, Damage);
                }
            }
            else if (isOnCooldown)
            {
                CooldownProgress += Time.deltaTime / cooldownTime;
                if (CooldownProgress >= 1f)
                {
                    CooldownProgress = 1f;
                    isOnCooldown = false;
                    ResetState();
                }
            }
        }

        public void StartCharging()
        {
            if (isCharging || isOnCooldown) return;

            isCharging = true;
            ChargeProgress = 0f;
            CombatEvents.OnWeaponCharging(gameObject, transform.parent.gameObject, WeaponType,
                transform.position, Vector3.zero, Damage);
        }

        public void Fire(Vector3 targetPosition)
        {
            if (!IsReady) return;

            CombatEvents.OnWeaponFired(gameObject, transform.parent.gameObject, WeaponType,
                transform.position, targetPosition, Damage);

            IsReady = false;
            isOnCooldown = true;
            CooldownProgress = 0f;
            CombatEvents.OnWeaponCooldown(gameObject, transform.parent.gameObject, WeaponType,
                transform.position, targetPosition, Damage);
        }

        private void ResetState()
        {
            ChargeProgress = 0f;
            CooldownProgress = 0f;
            isCharging = false;
            IsReady = false;
            isOnCooldown = false;
        }
    }
}
