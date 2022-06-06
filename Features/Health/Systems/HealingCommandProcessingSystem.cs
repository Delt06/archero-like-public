using Features.Health.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Health.Systems
{
    public class HealingCommandProcessingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealingCommand> _commandFilter = default;

        public void Run()
        {
            foreach (var i in _commandFilter)
            {
                ref var healingCommand = ref _commandFilter.Get1(i);
                var target = healingCommand.Target;
                if (!target.IsAlive() || target.Has<DeathTag>()) continue;

                ref var healthData = ref target.Get<HealthData>();
                if (Mathf.Approximately(healthData.Health, healthData.MaxHealth)) continue;

                var ratioOfMaxHealth = Mathf.Clamp01(healingCommand.RatioOfMaxHealth);
                var restoredHealth = healthData.MaxHealth * ratioOfMaxHealth;
                var oldHealth = healthData.Health;
                healthData.Health += restoredHealth;
                healthData.Health = Mathf.Clamp(healthData.Health, 0, healthData.MaxHealth);

                var actualRestoredHealth = healthData.Health - oldHealth;
                ref var healingEvent = ref target.Get<HealingEvent>();
                healingEvent.Target = target;
                healingEvent.RestoredHealth += actualRestoredHealth;
            }
        }
    }
}