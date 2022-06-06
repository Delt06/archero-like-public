using Features.Attack.Components;
using Features.Progression.Components.Upgrades;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Progression.Systems.Upgrades
{
    public class
        HandleCriticalStrikeProbabilityUpgradeSystem : HandlePlayerUpgradeSystem<CriticalStrikeProbabilityUpgrade,
            CriticalStrikeData>
    {
        protected override void ApplyUpgrade(in CriticalStrikeProbabilityUpgrade upgrade, EcsEntity entity,
            ref CriticalStrikeData affectedComponent)
        {
            var newProbability = Mathf.MoveTowards(affectedComponent.Probability, 1f, upgrade.ExtraProbability);
            affectedComponent.Probability = newProbability;
        }
    }
}