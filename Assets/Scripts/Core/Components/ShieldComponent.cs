using UnityEngine;

namespace FTL.Core.Components
{
    public class ShieldComponent : MonoBehaviour
    {
        public int MaxLayers { get; private set; }
        public int CurrentLayers { get; private set; }
        public bool HasShield { get; private set; }
        public float RechargeProgress { get; private set; }
        public bool IsRecharging { get; private set; }

        private float rechargeTime;

        public void Initialize(int layers, float rechargeDuration)
        {
            MaxLayers = layers;
            CurrentLayers = layers;
            rechargeTime = rechargeDuration;
            HasShield = layers > 0;
            IsRecharging = false;
            RechargeProgress = 0f;
        }

        private void Update()
        {
            if (IsRecharging)
            {
                RechargeProgress += Time.deltaTime / rechargeTime;
                if (RechargeProgress >= 1f)
                {
                    RechargeProgress = 1f;
                    IsRecharging = false;
                    CurrentLayers = MaxLayers;
                    HasShield = true;
                }
            }
        }

        public void TakeHit(Vector3 hitPosition)
        {
            if (!HasShield) return;

            CurrentLayers--;
            HasShield = CurrentLayers > 0;

            if (!HasShield)
            {
                StartRecharge();
            }
        }

        public void StartRecharge()
        {
            if (CurrentLayers >= MaxLayers) return;

            IsRecharging = true;
            RechargeProgress = 0f;
        }
    }
}
