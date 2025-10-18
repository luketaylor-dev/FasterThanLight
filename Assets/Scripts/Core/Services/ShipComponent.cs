using UnityEngine;

namespace FTL.Core.Services
{
    public class ShipComponent : MonoBehaviour
    {
        public float CurrentHealth { get; private set; }
        public float MaxHealth { get; private set; }
        public bool IsAlive { get; private set; } = true;

        public void Initialize(float health)
        {
            MaxHealth = health;
            CurrentHealth = health;
            IsAlive = true;
        }

        public void TakeDamage(float damage)
        {
            if (!IsAlive) return;

            CurrentHealth = Mathf.Max(0, CurrentHealth - damage);

            if (CurrentHealth <= 0)
            {
                IsAlive = false;
            }
        }

        public void Heal(float amount)
        {
            if (!IsAlive) return;

            CurrentHealth = Mathf.Min(MaxHealth, CurrentHealth + amount);
        }
    }
}
