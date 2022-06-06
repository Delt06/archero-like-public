using Features.Attack.Components;
using Features.Attack.Components.Projectiles;
using Leopotam.Ecs;

namespace Features.Attack.Systems.Projectiles
{
    public class ProjectileDataPropagationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ProjectileCreationEvent, CriticalStrikeData> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var projectileCreationEvent = ref _filter.Get1(i);
                ref var criticalStrikeData = ref _filter.Get2(i);
                var projectile = projectileCreationEvent.Projectile;
                projectile.Get<CriticalStrikeData>() = criticalStrikeData;
            }
        }
    }
}