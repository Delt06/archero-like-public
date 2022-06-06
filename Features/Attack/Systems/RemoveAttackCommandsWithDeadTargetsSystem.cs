using DELTation.LeoEcsExtensions.Utilities;
using Features.Attack.Components;
using Features.Health.Components;
using Leopotam.Ecs;

namespace Features.Attack.Systems
{
    public class RemoveAttackCommandsWithDeadTargetsSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackCommand> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var attackCommand = ref _filter.Get1(i);
                ref var takeDamageCommand = ref attackCommand.TakeDamageCommand;
                if (!takeDamageCommand.Target.TryGet(out DeathTag _)) continue;

                _filter.GetEntity(i).Del<AttackCommand>();
            }
        }
    }
}