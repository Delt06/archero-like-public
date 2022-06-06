using Features.Loot.Behaviour.Pooling;
using Features.Loot.Components;
using Features.Loot.Services;

namespace Features.Loot.Systems.Drop
{
    public class DropCoinLootSystem : DropLootSystemBase<CoinLootData, CoinLootViewPool>
    {
        public DropCoinLootSystem(CoinLootViewPool pool, ILootLocationGenerator locationGenerator) : base(pool,
            locationGenerator
        ) { }
    }
}