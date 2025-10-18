using UnityEngine;
using FTL.Core.Events;
using FTL.Core.Components;

namespace FTL.Core.Services
{
    public class ShieldService : IShieldService
    {
        private const float DEFAULT_RECHARGE_TIME = 5f;

        public ShieldComponent CreateShield(ShipComponent ship, int layers)
        {
            if (ship == null) return null;

            var shieldComponent = ship.gameObject.AddComponent<ShieldComponent>();
            shieldComponent.Initialize(layers, DEFAULT_RECHARGE_TIME);

            return shieldComponent;
        }

        public void DestroyShield(ShieldComponent shield)
        {
            if (shield == null) return;

            Object.Destroy(shield);
        }

        public bool HasShield(ShieldComponent shield)
        {
            return shield?.HasShield ?? false;
        }

        public int GetShieldLayers(ShieldComponent shield)
        {
            return shield?.CurrentLayers ?? 0;
        }

        public void DamageShield(ShieldComponent shield, Vector3 hitPosition)
        {
            shield?.TakeHit(hitPosition);
        }

        public void RechargeShield(ShieldComponent shield)
        {
            shield?.StartRecharge();
        }

        public float GetRechargeProgress(ShieldComponent shield)
        {
            return shield?.RechargeProgress ?? 0f;
        }

        public bool IsShieldRecharging(ShieldComponent shield)
        {
            return shield?.IsRecharging ?? false;
        }
    }
}
