using UnityEngine;

namespace FTL.Core.Services
{
    public class ProjectileService : IProjectileService
    {
        public ProjectileComponent CreateProjectile(Vector3 startPosition, Vector3 targetPosition, float speed, float damage)
        {
            var projectile = new GameObject("Projectile");
            projectile.transform.position = startPosition;

            var projectileComponent = projectile.AddComponent<ProjectileComponent>();
            projectileComponent.Initialize(targetPosition, speed, damage);

            return projectileComponent;
        }

        public void DestroyProjectile(ProjectileComponent projectile)
        {
            if (projectile == null) return;

            Object.Destroy(projectile.gameObject);
        }

        public void MoveProjectile(ProjectileComponent projectile, float deltaTime)
        {
            projectile?.Move(deltaTime);
        }

        public bool HasProjectileReachedTarget(ProjectileComponent projectile)
        {
            return projectile?.HasReachedTarget ?? true;
        }

        public float GetProjectileDamage(ProjectileComponent projectile)
        {
            return projectile?.Damage ?? 0f;
        }

        public Vector3 GetProjectilePosition(ProjectileComponent projectile)
        {
            return projectile?.transform.position ?? Vector3.zero;
        }

        public void SetProjectilePosition(ProjectileComponent projectile, Vector3 position)
        {
            if (projectile != null)
            {
                projectile.transform.position = position;
            }
        }
    }
}
