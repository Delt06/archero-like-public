using Features.Characters.Components;
using Features.Loot.Components;
using Features.Progression.Components;
using Leopotam.Ecs;

// ReSharper disable MemberCanBePrivate.Global

namespace Features.Loot.Systems.Collect
{
    public abstract class CollectLootSystemBase<TValue, TLootData, TAffectedData> : IEcsRunSystem
        where TLootData : struct where TAffectedData : struct
    {
        protected readonly EcsFilter<StageEndTag> StageEndFilter = default;
        protected readonly EcsFilter<TLootData, LootTag>.Exclude<LootCollectedTag> LootFilter = default;
        protected readonly EcsFilter<TAffectedData, PlayerTag> PlayerFilter = default;

        public void Run()
        {
            foreach (var _ in StageEndFilter)
            {
                var aggregatedValue = InitialAggregateValue;

                foreach (var i in LootFilter)
                {
                    ref var lootData = ref LootFilter.Get1(i);
                    Aggregate(ref aggregatedValue, lootData);
                    LootFilter.GetEntity(i).Get<LootCollectedTag>();
                }

                if (IsNotZero(aggregatedValue))
                    foreach (var i in PlayerFilter)
                    {
                        ref var affectedData = ref PlayerFilter.Get1(i);
                        var entity = PlayerFilter.GetEntity(i);
                        AffectData(ref affectedData, entity, aggregatedValue);
                    }

                break;
            }
        }

        protected virtual TValue InitialAggregateValue => default;

        protected abstract void Aggregate(ref TValue existingValue, in TLootData lootData);

        protected abstract bool IsNotZero(TValue value);

        protected abstract void AffectData(ref TAffectedData affectedData, in EcsEntity entity, TValue aggregatedValue);
    }
}