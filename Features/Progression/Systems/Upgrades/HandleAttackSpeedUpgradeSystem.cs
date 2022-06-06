using Features.Attack.Components;
using Features.Progression.Components.Upgrades;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Upgrades
{
    public class HandleAttackSpeedUpgradeSystem : HandlePlayerUpgradeSystem<AttackSpeedUpgrade, AttackDurationData>
    {
        protected override void ApplyUpgrade(in AttackSpeedUpgrade upgrade, EcsEntity entity,
            ref AttackDurationData affectedComponent)
        {
            affectedComponent.Duration /= upgrade.Multiplier;
        }
    }
}