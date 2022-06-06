using Features.Progression.Components.Upgrades;
using Features.Progression.Systems.Upgrades;
using UnityEngine;

namespace Features.Progression.Assets.Upgrades
{
    [CreateAssetMenu(menuName = AssetPath + "Combo Damage")]
    public class ComboDamageUpgradeAsset : UpgradeAsset<ComboDamageUpgrade>
    {
        protected override HandleUpgradeSystem<ComboDamageUpgrade> CreateHandleSystem() =>
            new HandleComboDamageUpgradeSystem();
    }
}