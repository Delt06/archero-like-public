using Features.Loot.Components;
using Features.Progression.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Loot.Systems.Collect
{
    public class CollectExperienceLootSystem : CollectLootSystemBase<float, ExperienceLootData, ExperienceData>
    {
        protected override void Aggregate(ref float existingValue, in ExperienceLootData lootData)
        {
            existingValue += lootData.Amount;
        }

        protected override bool IsNotZero(float value) => !Mathf.Approximately(value, 0f);

        protected override void AffectData(ref ExperienceData affectedData, in EcsEntity entity, float aggregatedValue)
        {
            affectedData.Experience += aggregatedValue;
            entity.Get<ExperienceIncreaseEvent>();
        }
    }
}