using Features.Coins.Components;
using Features.Loot.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Loot.Systems.Collect
{
    public class CollectCoinLootSystem : CollectLootSystemBase<int, CoinLootData, CoinsData>
    {
        protected override void Aggregate(ref int existingValue, in CoinLootData lootData)
        {
            var count = Random.Range(lootData.MinCount, lootData.MaxCount);
            existingValue += count;
        }

        protected override bool IsNotZero(int value) => value != 0;

        protected override void AffectData(ref CoinsData affectedData, in EcsEntity entity, int aggregatedValue)
        {
            affectedData.Count += aggregatedValue;
            entity.Get<CoinsDataChangeEvent>().Amount = aggregatedValue;
        }
    }
}