using Features.Attack.Components;
using Features.Combo.Components;
using Features.Combo.Components.Effects;
using Leopotam.Ecs;

namespace Features.Combo.Systems.Effects
{
    public class HandleBurningComboEffectCommandSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ComboBurningEffectData, ComboHitEvent, DamageData,
                ComboEffectCommand<ComboBurningEffectData>>
            _filter = default;
        private readonly EcsWorld _world = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var comboBurningEffectData = ref _filter.Get1(i);
                ref var comboHitEvent = ref _filter.Get2(i);
                ref var damageData = ref _filter.Get3(i);

                var burningEntity = _world.NewEntity();
                ref var burningData = ref burningEntity.Get<BurningData>();
                burningData.Damage = comboBurningEffectData.DamageMultiplier * damageData.Damage;
                burningData.Period = comboBurningEffectData.DamagePeriod;
                burningData.Target = comboHitEvent.Target;
                burningData.RemainingTime = comboBurningEffectData.Duration;
                burningData.TimeTillNextDamage = comboBurningEffectData.DamagePeriod;
            }
        }
    }
}