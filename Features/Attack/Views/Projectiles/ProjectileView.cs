using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Views;
using Leopotam.Ecs;

namespace Features.Attack.Views.Projectiles
{
    public class ProjectileView : EntityView
    {
        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            var viewBackRef = new ViewBackRef<ProjectileView> { View = this };
            entity.Get<ViewBackRef<ProjectileView>>() = viewBackRef;
        }
    }
}