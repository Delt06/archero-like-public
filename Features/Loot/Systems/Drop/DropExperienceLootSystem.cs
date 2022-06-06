using Features.Loot.Behaviour.Pooling;
using Features.Loot.Components;
using Features.Loot.Services;

namespace Features.Loot.Systems.Drop
{
    public class DropExperienceLootSystem : DropLootSystemBase<ExperienceLootData, ExperienceLootViewPool>
    {
        public DropExperienceLootSystem(ExperienceLootViewPool pool, ILootLocationGenerator locationGenerator) : base(
            pool,
            locationGenerator
        ) { }
    }
}