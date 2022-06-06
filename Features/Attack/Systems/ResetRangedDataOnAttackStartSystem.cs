using Features.Attack.Components;
using Leopotam.Ecs;

namespace Features.Attack.Systems
{
    public class ResetRangedDataOnAttackStartSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RangedData, AttackStartEvent> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                _filter.Get1(i).Shot = false;
            }
        }
    }
}