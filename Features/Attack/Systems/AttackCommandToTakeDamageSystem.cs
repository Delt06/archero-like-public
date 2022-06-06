using Features.Attack.Components;
using Leopotam.Ecs;

namespace Features.Attack.Systems
{
    public class AttackCommandToTakeDamageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackCommand> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var attackCommand = ref _filter.Get1(i);
                _filter.GetEntity(i).Get<TakeDamageCommand>() = attackCommand.TakeDamageCommand;
            }
        }
    }
}