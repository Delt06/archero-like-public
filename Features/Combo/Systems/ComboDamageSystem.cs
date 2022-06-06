using DELTation.LeoEcsExtensions.Utilities;
using Features.Attack.Components;
using Features.Combo.Components;
using Leopotam.Ecs;

namespace Features.Combo.Systems
{
    public class ComboDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackCommand> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var attackCommand = ref _filter.Get1(i);
                var commandCreator = attackCommand.Creator;
                if (commandCreator.Has<NoComboTag>()) continue;
                if (!commandCreator.TryGet(out ComboData comboData)) continue;
                if (!commandCreator.TryGet(out RuntimeComboData runtimeComboData)) continue;

                var extraDamageOverHits = comboData.ExtraDamageOverHits;
                var hits = runtimeComboData.Hits;
                var damageMultiplier = 1f + extraDamageOverHits.Evaluate(hits) * comboData.ExtraMultiplier;
                attackCommand.TakeDamageCommand.Damage *= damageMultiplier;
            }
        }
    }
}