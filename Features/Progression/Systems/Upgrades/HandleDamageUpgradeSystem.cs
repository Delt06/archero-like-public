using Features.Attack.Components;
using Features.Progression.Components.Upgrades;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Upgrades
{
    public class HandleDamageUpgradeSystem : HandlePlayerUpgradeSystem<DamageUpgrade, DamageData>
    {
        protected override void ApplyUpgrade(in DamageUpgrade upgrade, EcsEntity entity,
            ref DamageData affectedComponent)
        {
            affectedComponent.Damage *= upgrade.Multiplier;
        }
    }
}