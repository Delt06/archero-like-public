using Features.Combo.Components;
using Features.TimeUpdate.Services;
using Leopotam.Ecs;

namespace Features.Combo.Systems
{
    public class ComboTimerSystem : IEcsRunSystem
    {
        private readonly EcsFilter<RuntimeComboData>.Exclude<NoComboTag> _filter = default;
        private readonly ITime _time;

        public ComboTimerSystem(ITime time) => _time = time;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var comboData = ref _filter.Get1(i);
                comboData.TimeUntilReset -= _time.DeltaTime;
                if (comboData.TimeUntilReset > 0f) continue;

                comboData.Hits = 0;
                var entity = _filter.GetEntity(i);
                entity.Get<NoComboTag>();
            }
        }
    }
}