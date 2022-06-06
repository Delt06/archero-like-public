using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Pooling;
using Features.Health.Components;
using Features.Loot.Components;
using Features.Loot.Services;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Loot.Systems.Drop
{
    public abstract class DropLootSystemBase<TLootData, TPool> : IEcsRunSystem where TLootData : struct
        where TPool : IEntityViewPool
    {
        // ReSharper disable once MemberCanBePrivate.Global
        protected readonly EcsFilter<TLootData, Position, DeathEvent> Filter = default;

        private readonly TPool _pool;
        private readonly ILootLocationGenerator _locationGenerator;

        protected DropLootSystemBase(TPool pool, ILootLocationGenerator locationGenerator)
        {
            _pool = pool;
            _locationGenerator = locationGenerator;
        }

        public void Run()
        {
            foreach (var i in Filter)
            {
                ref var lootData = ref Filter.Get1(i);
                if (!ShouldDrop(lootData)) continue;

                var deathPosition = Filter.Get2(i).WorldPosition;
                var (position, rotation) = _locationGenerator.GetLocation(deathPosition);
                var lootEntityView = _pool.Create(position, rotation);
                var entity = lootEntityView.Entity;
                entity.Get<TLootData>() = lootData;
                entity.Get<LootTag>();
            }
        }

        protected virtual bool ShouldDrop(in TLootData lootData) => true;
    }
}