using Features.Progression.Components.Upgrades;
using Features.Progression.Systems.Upgrades;
using UnityEngine;

namespace Features.Progression.Assets.Upgrades
{
    [CreateAssetMenu(menuName = AssetPath + "Attack Speed")]
    public class AttackSpeedUpgradeAsset : UpgradeAsset<AttackSpeedUpgrade>
    {
        protected override HandleUpgradeSystem<AttackSpeedUpgrade> CreateHandleSystem() =>
            new HandleAttackSpeedUpgradeSystem();
    }
}