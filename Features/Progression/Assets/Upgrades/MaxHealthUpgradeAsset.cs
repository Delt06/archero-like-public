using Features.Progression.Components.Upgrades;
using Features.Progression.Systems.Upgrades;
using UnityEngine;

namespace Features.Progression.Assets.Upgrades
{
    [CreateAssetMenu(menuName = AssetPath + "Max Health")]
    public class MaxHealthUpgradeAsset : UpgradeAsset<MaxHealthUpgrade>
    {
        protected override HandleUpgradeSystem<MaxHealthUpgrade> CreateHandleSystem() =>
            new HandleMaxHealthUpgradeSystem();
    }
}