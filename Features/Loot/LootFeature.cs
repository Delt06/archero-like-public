using DELTation.LeoEcsExtensions.Composition.Di;
using DELTation.LeoEcsExtensions.Features;
using Features.Loot.Systems;
using Features.Loot.Systems.Collect;
using Features.Loot.Systems.Drop;
using Leopotam.Ecs;

namespace Features.Loot
{
    public class LootFeature : Feature
    {
        public override void Register(EcsSystems systems, EcsSystems physicsSystems)
        {
            systems
                .CreateAndAdd<DropExperienceLootSystem>()
                .CreateAndAdd<DropCoinLootSystem>()
                .CreateAndAdd<DropHealingLootSystem>()
                ;
            systems
                .CreateAndAdd<CollectExperienceLootSystem>()
                .CreateAndAdd<CollectCoinLootSystem>()
                .CreateAndAdd<CollectHealingLootSystem>()
                ;
            systems.CreateAndAdd<DisposePooledCollectedLootSystem>()
                ;
        }
    }
}