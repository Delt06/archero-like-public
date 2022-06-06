using Features.Progression.Components.Upgrades;
using Features.Progression.Systems.Upgrades;
using UnityEngine;

namespace Features.Progression.Assets.Upgrades
{
    [CreateAssetMenu(menuName = AssetPath + "Critical Attack Probability")]
    public class CriticalStrikeProbabilityUpgradeAsset : UpgradeAsset<CriticalStrikeProbabilityUpgrade>
    {
        protected override HandleUpgradeSystem<CriticalStrikeProbabilityUpgrade> CreateHandleSystem() =>
            new HandleCriticalStrikeProbabilityUpgradeSystem();
    }
}