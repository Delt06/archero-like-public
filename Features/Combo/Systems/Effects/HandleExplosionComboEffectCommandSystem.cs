using DELTation.LeoEcsExtensions.Components;
using Features.Attack.Components;
using Features.Cameras.Components;
using Features.Characters.Components;
using Features.Combo.Components.Effects;
using Features.Movement.Components;
using Leopotam.Ecs;

namespace Features.Combo.Systems.Effects
{
    public class HandleExplosionComboEffectCommandSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ComboExplosionEffectData, Position, TeamData, DamageData,
                ComboEffectCommand<ComboExplosionEffectData>>
            _filter = default;
        private readonly EcsWorld _world = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var explosionEffectData = ref _filter.Get1(i);
                ref var position = ref _filter.Get2(i);
                ref var teamData = ref _filter.Get3(i);
                ref var damageData = ref _filter.Get4(i);

                var explosionEntity = _world.NewEntity();
                ref var explosionData = ref explosionEntity.Get<ExplosionData>();
                explosionData.Center = position.WorldPosition;
                explosionData.Damage = damageData.Damage * explosionEffectData.DamageMultiplier;
                explosionData.Team = teamData.Team;
                explosionData.PushRadius = explosionEffectData.Radius;

                var shakeEntity = _world.NewEntity();
                shakeEntity.Get<CameraShakeCommand>();
            }
        }
    }
}