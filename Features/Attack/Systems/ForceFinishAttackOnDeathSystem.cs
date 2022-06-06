using Features.Attack.Components;
using Features.Health.Components;
using Leopotam.Ecs;

namespace Features.Attack.Systems
{
    public class ForceFinishAttackOnDeathSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackData, DeathTag> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var attackData = ref _filter.Get1(i);
                if (!attackData.Attacking) continue;

                attackData.AutoReset(ref attackData);
                var entity = _filter.GetEntity(i);
                entity.Get<AttackFinishEvent>();
            }
        }
    }
}