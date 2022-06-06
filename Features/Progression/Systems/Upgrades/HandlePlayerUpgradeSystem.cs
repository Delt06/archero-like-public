using Features.Characters.Components;
using Features.Progression.Components.Upgrades;
using Features.Progression.Services.Upgrades;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Upgrades
{
    public abstract class HandlePlayerUpgradeSystem<TUpgrade, TAffectedComponent> : HandleUpgradeSystem<TUpgrade>
        where TAffectedComponent : struct where TUpgrade : struct
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly EcsFilter<TAffectedComponent, PlayerTag> Filter = default;
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly EcsWorld World = default;

        protected sealed override void ApplyUpgrade(in TUpgrade upgrade, IUpgradeInfo info)
        {
            foreach (var i in Filter)
            {
                ref var affectedComponent = ref Filter.Get1(i);
                var entity = Filter.GetEntity(i);
                ApplyUpgrade(upgrade, entity, ref affectedComponent);

                var upgradeEventEntity = World.NewEntity();
                ref var appliedUpgradeEvent = ref upgradeEventEntity.Get<AppliedUpgradeEvent>();
                appliedUpgradeEvent.UpgradeInfo = info;
            }
        }

        protected abstract void ApplyUpgrade(in TUpgrade upgrade, EcsEntity entity,
            ref TAffectedComponent affectedComponent);
    }

    public abstract class HandlePlayerUpgradeSystem<TUpgrade> : HandleUpgradeSystem<TUpgrade> where TUpgrade : struct
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly EcsFilter<PlayerTag> Filter = default;
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly EcsWorld World = default;

        protected sealed override void ApplyUpgrade(in TUpgrade upgrade, IUpgradeInfo info)
        {
            foreach (var i in Filter)
            {
                var entity = Filter.GetEntity(i);
                ApplyUpgrade(upgrade, entity);

                var upgradeEventEntity = World.NewEntity();
                ref var appliedUpgradeEvent = ref upgradeEventEntity.Get<AppliedUpgradeEvent>();
                appliedUpgradeEvent.UpgradeInfo = info;
            }
        }

        protected abstract void ApplyUpgrade(in TUpgrade upgrade, EcsEntity entity);
    }
}