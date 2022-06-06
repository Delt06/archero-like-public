using Features.Progression.Components.Upgrades;
using Features.Progression.Systems.Upgrades;
using UnityEngine;

namespace Features.Progression.Assets.Upgrades
{
    [CreateAssetMenu(menuName = AssetPath + "Combo Burning")]
    public class ComboBurningUpgradeAsset : UpgradeAsset<ComboBurningUpgrade>
    {
        protected override HandleUpgradeSystem<ComboBurningUpgrade> CreateHandleSystem() =>
            new HandleComboBurningUpgradeSystem();
    }
}