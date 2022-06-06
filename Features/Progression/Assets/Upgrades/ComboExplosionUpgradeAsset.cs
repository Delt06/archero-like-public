using Features.Progression.Components.Upgrades;
using Features.Progression.Systems.Upgrades;
using UnityEngine;

namespace Features.Progression.Assets.Upgrades
{
    [CreateAssetMenu(menuName = AssetPath + "Combo Explosion")]
    public class ComboExplosionUpgradeAsset : UpgradeAsset<ComboExplosionUpgrade>
    {
        protected override HandleUpgradeSystem<ComboExplosionUpgrade> CreateHandleSystem() =>
            new HandleComboExplosionUpgradeSystem();
    }
}