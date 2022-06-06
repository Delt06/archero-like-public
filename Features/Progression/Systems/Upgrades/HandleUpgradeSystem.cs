using Features.Progression.Components.Upgrades;
using Features.Progression.Services.Upgrades;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Upgrades
{
    public abstract class HandleUpgradeSystem<TUpgrade> : IEcsRunSystem where TUpgrade : struct
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly EcsFilter<TUpgrade, UpgradeInfoData> UpgradeFilter = default;

        public void Run()
        {
            foreach (var i in UpgradeFilter)
            {
                ref var upgrade = ref UpgradeFilter.Get1(i);
                ref var upgradeInfoData = ref UpgradeFilter.Get2(i);
                ApplyUpgrade(upgrade, upgradeInfoData.UpgradeInfo);
            }
        }

        protected abstract void ApplyUpgrade(in TUpgrade upgrade, IUpgradeInfo info);
    }
}