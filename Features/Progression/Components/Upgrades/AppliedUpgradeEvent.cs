using Features.Progression.Services.Upgrades;
using Leopotam.Ecs;

namespace Features.Progression.Components.Upgrades
{
    public struct AppliedUpgradeEvent : IEcsAutoReset<AppliedUpgradeEvent>
    {
        public IUpgradeInfo UpgradeInfo;

        public void AutoReset(ref AppliedUpgradeEvent c)
        {
            c.UpgradeInfo = null;
        }
    }
}