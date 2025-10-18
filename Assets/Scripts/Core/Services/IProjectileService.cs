using UnityEngine;

namespace FTL.Core.Services
{
    public interface IProjectileService
    {
        ProjectileComponent CreateProjectile(Vector3 startPosition, Vector3 targetPosition, float speed, float damage);
        void DestroyProjectile(ProjectileComponent projectile);
        void MoveProjectile(ProjectileComponent projectile, float deltaTime);
        bool HasProjectileReachedTarget(ProjectileComponent projectile);
        float GetProjectileDamage(ProjectileComponent projectile);
        Vector3 GetProjectilePosition(ProjectileComponent projectile);
        void SetProjectilePosition(ProjectileComponent projectile, Vector3 position);
    }
}
