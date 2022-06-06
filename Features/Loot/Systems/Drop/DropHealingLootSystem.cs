using Features.Loot.Behaviour.Pooling;
using Features.Loot.Components;
using Features.Loot.Services;
using UnityEngine;

namespace Features.Loot.Systems.Drop
{
    public class DropHealingLootSystem : DropLootSystemBase<HealingLootData, HealingLootViewPool>
    {
        public DropHealingLootSystem(HealingLootViewPool pool, ILootLocationGenerator locationGenerator) : base(pool,
            locationGenerator
        ) { }

        protected override bool ShouldDrop(in HealingLootData lootData) => Random.value <= lootData.Probability;
    }
}