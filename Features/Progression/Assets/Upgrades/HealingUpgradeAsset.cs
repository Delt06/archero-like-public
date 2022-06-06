using Features.Progression.Components.Upgrades;
using Features.Progression.Systems.Upgrades;
using UnityEngine;

namespace Features.Progression.Assets.Upgrades
{
    [CreateAssetMenu(menuName = AssetPath + "Heal")]
    public class HealingUpgradeAsset : UpgradeAsset<HealingUpgrade>
    {
        protected override HandleUpgradeSystem<HealingUpgrade> CreateHandleSystem() => new HandleHealingUpgradeSystem();
    }
}