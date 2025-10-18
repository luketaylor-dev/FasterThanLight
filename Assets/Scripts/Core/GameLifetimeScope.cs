using VContainer;
using VContainer.Unity;
using FTL.Core.Services;

namespace FTL.Core
{
    public class GameLifetimeScope : LifetimeScope
    {
        protected override void Configure(IContainerBuilder builder)
        {
            builder.Register<IShipService, ShipService>(Lifetime.Singleton);
            builder.Register<IWeaponService, WeaponService>(Lifetime.Singleton);
            builder.Register<IRoomService, RoomService>(Lifetime.Singleton);
            builder.Register<IShieldService, ShieldService>(Lifetime.Singleton);
            builder.Register<IProjectileService, ProjectileService>(Lifetime.Singleton);
            builder.Register<ICombatService, CombatService>(Lifetime.Singleton);
            builder.Register<IAIService, AIService>(Lifetime.Singleton);
        }
    }
}
