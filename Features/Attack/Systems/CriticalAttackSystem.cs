using Features.Attack.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Attack.Systems
{
    public class CriticalAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackCommand> _filter = default;
        private readonly EcsWorld _world = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var attackCommand = ref _filter.Get1(i);
                if (!attackCommand.Creator.IsAlive()) continue;
                if (!attackCommand.Creator.Has<CriticalStrikeData>()) continue;

                ref var criticalStrikeData = ref attackCommand.Creator.Get<CriticalStrikeData>();
                if (Random.value > criticalStrikeData.Probability) continue;

                ref var takeDamageCommand = ref attackCommand.TakeDamageCommand;
                takeDamageCommand.Damage *= criticalStrikeData.DamageMultiplier;
                _filter.GetEntity(i).Get<CriticalAttackTag>();

                var eventEntity = _world.NewEntity();
                eventEntity.Get<CriticalStrikeEvent>().Target = takeDamageCommand.Target;
            }
        }
    }
}