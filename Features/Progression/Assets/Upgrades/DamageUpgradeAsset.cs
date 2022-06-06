using Features.Progression.Components.Upgrades;
using Features.Progression.Systems.Upgrades;
using UnityEngine;

namespace Features.Progression.Assets.Upgrades
{
    [CreateAssetMenu(menuName = AssetPath + "Damage")]
    public class DamageUpgradeAsset : UpgradeAsset<DamageUpgrade>
    {
        protected override HandleUpgradeSystem<DamageUpgrade> CreateHandleSystem() => new HandleDamageUpgradeSystem();
    }
}