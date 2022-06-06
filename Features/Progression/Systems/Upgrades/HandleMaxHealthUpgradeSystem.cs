using Features.Health.Components;
using Features.Progression.Components.Upgrades;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Upgrades
{
    public class HandleMaxHealthUpgradeSystem : HandlePlayerUpgradeSystem<MaxHealthUpgrade, HealthData>
    {
        protected override void ApplyUpgrade(in MaxHealthUpgrade upgrade, EcsEntity entity,
            ref HealthData affectedComponent)
        {
            var oldRatio = affectedComponent.Health / affectedComponent.MaxHealth;
            affectedComponent.MaxHealth *= upgrade.Multiplier;
            affectedComponent.Health = affectedComponent.MaxHealth * oldRatio;
        }
    }
}