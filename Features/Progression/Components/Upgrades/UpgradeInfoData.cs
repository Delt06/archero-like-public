using Features.Progression.Services.Upgrades;
using Leopotam.Ecs;

namespace Features.Progression.Components.Upgrades
{
    public struct UpgradeInfoData : IEcsAutoReset<UpgradeInfoData>
    {
        public IUpgradeInfo UpgradeInfo;

        public void AutoReset(ref UpgradeInfoData c)
        {
            c.UpgradeInfo = null;
        }
    }
}