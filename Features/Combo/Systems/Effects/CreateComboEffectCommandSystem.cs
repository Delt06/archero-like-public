using Features.Combo.Components;
using Features.Combo.Components.Effects;
using Leopotam.Ecs;

namespace Features.Combo.Systems.Effects
{
    public sealed class CreateComboEffectCommandSystem<TEffectData> : IEcsRunSystem
        where TEffectData : struct, IComboEffectData
    {
        private readonly EcsFilter<ComboHitEvent, TEffectData> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var comboHitEvent = ref _filter.Get1(i);
                ref var comboEffectData = ref _filter.Get2(i);
                if (comboHitEvent.Hits % comboEffectData.Period != 0) continue;

                _filter.GetEntity(i).Get<ComboEffectCommand<TEffectData>>();
            }
        }
    }
}