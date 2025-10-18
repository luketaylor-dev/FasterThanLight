using UnityEngine;

namespace FTL.Core.Components
{
    public class ProjectileComponent : MonoBehaviour
    {
        public Vector3 TargetPosition { get; private set; }
        public float Speed { get; private set; }
        public float Damage { get; private set; }
        public bool HasReachedTarget { get; private set; }

        public void Initialize(Vector3 target, float projectileSpeed, float projectileDamage)
        {
            TargetPosition = target;
            Speed = projectileSpeed;
            Damage = projectileDamage;
            HasReachedTarget = false;
        }

        public void Move(float deltaTime)
        {
            if (HasReachedTarget) return;

            Vector3 direction = (TargetPosition - transform.position).normalized;
            float distanceToTarget = Vector3.Distance(transform.position, TargetPosition);
            float moveDistance = Speed * deltaTime;

            if (moveDistance >= distanceToTarget)
            {
                transform.position = TargetPosition;
                HasReachedTarget = true;
            }
            else
            {
                transform.position += direction * moveDistance;
            }
        }
    }
}
