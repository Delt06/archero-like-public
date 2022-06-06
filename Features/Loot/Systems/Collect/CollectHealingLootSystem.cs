using Features.Health.Components;
using Features.Loot.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Loot.Systems.Collect
{
    public class CollectHealingLootSystem : CollectLootSystemBase<float, HealingLootData, HealthData>
    {
        private readonly EcsWorld _world = default;

        protected override void Aggregate(ref float existingValue, in HealingLootData lootData)
        {
            existingValue += Random.Range(lootData.MinAmount, lootData.MaxAmount);
        }

        protected override bool IsNotZero(float value) => !Mathf.Approximately(value, 0f);

        protected override void AffectData(ref HealthData affectedData, in EcsEntity entity, float aggregatedValue)
        {
            var healingEntity = _world.NewEntity();
            ref var healingCommand = ref healingEntity.Get<HealingCommand>();
            healingCommand.RatioOfMaxHealth = aggregatedValue;
            healingCommand.Target = entity;
        }
    }
}