using Features.Attack.Components;
using Leopotam.Ecs;

namespace Features.Attack.Systems
{
    public class AttackZoneClearSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackZoneData, AttackStartEvent> _startFilter = default;
        private readonly EcsFilter<AttackZoneData, AttackFinishEvent> _finishFilter = default;

        public void Run()
        {
            foreach (var i in _finishFilter)
            {
                ref var zone = ref _finishFilter.Get1(i);
                zone.AlreadyDealtDamage.Clear();
            }

            foreach (var i in _startFilter)
            {
                ref var zone = ref _startFilter.Get1(i);
                zone.AlreadyDealtDamage.Clear();
            }
        }
    }
}