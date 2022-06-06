using Features.Attack.Components;
using Features.Movement.Components;
using Leopotam.Ecs;

namespace Features.Attack.Systems.Steady
{
    public class BlockAttackWhenNotSteadySystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovementData, AttackStartEvent, SteadyAttackTag> _startFilter = default;
        private readonly EcsFilter<MovementData, AttackData, SteadyAttackTag> _attackFilter = default;

        public void Run()
        {
            foreach (var iStart in _startFilter)
            {
                ref var movementData = ref _startFilter.Get1(iStart);
                if (!movementData.IsMoving) continue;

                var entity = _startFilter.GetEntity(iStart);
                entity.Del<AttackStartEvent>();
            }

            foreach (var iAttack in _attackFilter)
            {
                ref var movementData = ref _startFilter.Get1(iAttack);
                if (!movementData.IsMoving) continue;

                ref var attackData = ref _attackFilter.Get2(iAttack);
                if (!attackData.Attacking) continue;
                if (attackData.DealingDamage) continue;

                attackData.AutoReset(ref attackData);
                var entity = _attackFilter.GetEntity(iAttack);
                entity.Get<AttackFinishEvent>();
            }
        }
    }
}