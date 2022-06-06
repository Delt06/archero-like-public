using Features.Health.Components;
using Features.Progression.Components.Upgrades;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Progression.Systems.Upgrades
{
    public class HandleHealingUpgradeSystem : HandlePlayerUpgradeSystem<HealingUpgrade, HealthData>
    {
        protected override void ApplyUpgrade(in HealingUpgrade upgrade, EcsEntity entity,
            ref HealthData affectedComponent)
        {
            var healingEntity = World.NewEntity();
            ref var healingCommand = ref healingEntity.Get<HealingCommand>();
            healingCommand.RatioOfMaxHealth = upgrade.HealProportion;
            healingCommand.Target = entity;
        }
    }
}