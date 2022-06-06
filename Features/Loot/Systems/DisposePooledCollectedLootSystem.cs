using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Pooling;
using Features.Loot.Components;
using Features.Loot.Views;
using Leopotam.Ecs;

namespace Features.Loot.Systems
{
    public class DisposePooledCollectedLootSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ViewBackRef<LootView>, PoolBackRef, LootCollectedTag> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                LootView view = _filter.Get1(i);
                EntityViewPool pool = _filter.Get2(i);
                pool.Dispose(view);
            }
        }
    }
}