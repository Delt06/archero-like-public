using Features.Attack.Components;
using Features.Combo.Components;
using Leopotam.Ecs;

namespace Features.Combo.Systems
{
    public class ResetComboTimerOnMissSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RuntimeComboData, AttackStartEvent> _startFilter = default;
        private readonly EcsFilter<RuntimeComboData, AttackFinishEvent> _finishFilter = default;

        public void Run()
        {
            foreach (var i in _startFilter)
            {
                _startFilter.Get1(i).HitOnThisAttack = false;
            }

            foreach (var i in _finishFilter)
            {
                ref var comboData = ref _finishFilter.Get1(i);
                if (!comboData.HitOnThisAttack)
                    comboData.TimeUntilReset = 0f;
                comboData.HitOnThisAttack = false;
            }
        }
    }
}