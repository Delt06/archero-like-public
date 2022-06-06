using Features.Attack.Components;
using Features.Combo.Components;
using Leopotam.Ecs;

namespace Features.Combo.Systems
{
    public class IncreaseComboOnAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackCommand> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var attackCommand = ref _filter.Get1(i);
                var attacker = attackCommand.Creator;
                if (!attacker.Has<ComboData>()) continue;
                if (!attacker.Has<RuntimeComboData>()) continue;

                ref var comboData = ref attacker.Get<ComboData>();
                ref var runtimeComboData = ref attacker.Get<RuntimeComboData>();
                if (runtimeComboData.HitOnThisAttack) continue;

                runtimeComboData.TimeUntilReset = comboData.Cooldown;
                runtimeComboData.Hits++;
                runtimeComboData.HitOnThisAttack = true;
                ref var comboHitEvent = ref attacker.Get<ComboHitEvent>();
                comboHitEvent.Hits = runtimeComboData.Hits;
                comboHitEvent.Target = attackCommand.TakeDamageCommand.Target;
                attacker.Del<NoComboTag>();
            }
        }
    }
}