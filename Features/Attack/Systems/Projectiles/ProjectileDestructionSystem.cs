using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Pooling;
using Features.Attack.Components.Projectiles;
using Features.Attack.Views.Projectiles;
using Leopotam.Ecs;

namespace Features.Attack.Systems.Projectiles
{
    public class ProjectileDestructionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ViewBackRef<ProjectileView>, PoolBackRef, ProjectileDestructionCommand> _filter =
            default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ProjectileView view = _filter.Get1(i);
                EntityViewPool pool = _filter.Get2(i);
                pool.Dispose(view);
            }
        }
    }
}