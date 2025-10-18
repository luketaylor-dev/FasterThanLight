using UnityEngine;
using FTL.Core.Events;
using FTL.Core.Components;

namespace FTL.Core.Services
{
    public interface IShieldService
    {
        ShieldComponent CreateShield(ShipComponent ship, int layers);
        void DestroyShield(ShieldComponent shield);
        bool HasShield(ShieldComponent shield);
        int GetShieldLayers(ShieldComponent shield);
        void DamageShield(ShieldComponent shield, Vector3 hitPosition);
        void RechargeShield(ShieldComponent shield);
        float GetRechargeProgress(ShieldComponent shield);
        bool IsShieldRecharging(ShieldComponent shield);
    }
}
