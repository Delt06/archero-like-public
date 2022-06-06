using Features.Attack.Components;
using Features.Attack.Components.Projectiles;
using Leopotam.Ecs;

namespace Features.Attack.Systems.Projectiles
{
    public class ProjectileDealDamageOnHitSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ProjectileData, ProjectileHitEvent> _filter = default;
        private readonly EcsWorld _world = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var projectileHitEvent = _filter.Get2(i);
                var target = projectileHitEvent.Target;
                ref var projectileData = ref _filter.Get1(i);
                ref var command = ref _world.NewEntity().Get<AttackCommand>();
                command.Creator = _filter.GetEntity(i);
                command.TakeDamageCommand = new TakeDamageCommand
                {
                    Damage = projectileData.Damage,
                    SourceTeam = projectileData.Team,
                    Target = target,
                };

                var projectileEntity = _filter.GetEntity(i);
                var projectileDestructionCommand = new ProjectileDestructionCommand();
                projectileEntity.Get<ProjectileDestructionCommand>() = projectileDestructionCommand;
            }
        }
    }
}