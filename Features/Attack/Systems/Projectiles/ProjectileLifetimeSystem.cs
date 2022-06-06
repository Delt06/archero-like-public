using DELTation.LeoEcsExtensions.Components;
using Features.Attack.Components.Projectiles;
using Features.Attack.Views.Projectiles;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Attack.Systems.Projectiles
{
    public class ProjectileLifetimeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ProjectileData, ViewBackRef<ProjectileView>> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var projectileData = ref _filter.Get1(i);
                projectileData.RemainingTime -= Time.deltaTime;
                if (projectileData.RemainingTime > 0f) continue;

                var entity = _filter.GetEntity(i);
                entity.Get<ProjectileDestructionCommand>() = new ProjectileDestructionCommand();
            }
        }
    }
}