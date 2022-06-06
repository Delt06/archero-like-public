using Features.Attack.Components;
using Features.Characters.Components;
using Features.Health.Components;
using Leopotam.Ecs;

namespace Features.Health.Systems
{
    public class TakeDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TakeDamageCommand> _commands = default;

        public void Run()
        {
            foreach (var i in _commands)
            {
                ref var command = ref _commands.Get1(i);
                var target = command.Target;
                if (!target.IsAlive()) continue;

                ref var teamData = ref target.Get<TeamData>();
                if (teamData.Team == command.SourceTeam) continue;

                ref var healthData = ref target.Get<HealthData>();
                if (!healthData.IsAlive) continue;

                DealDamage(command, ref healthData, target, _commands.GetEntity(i));
            }
        }

        private static void DealDamage(TakeDamageCommand command, ref HealthData healthData, in EcsEntity target,
            in EcsEntity commandEntity)
        {
            var damage = command.Damage;
            healthData.Health -= damage;
            ref var takeDamageEvent = ref commandEntity.Get<TakeDamageEvent>();
            takeDamageEvent.Damage = damage;
            takeDamageEvent.Target = target;
        }
    }
}