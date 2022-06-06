using DELTation.LeoEcsExtensions.Views;
using Leopotam.Ecs;

namespace Features.Loot.Views
{
    public class LootView : EntityView
    {
        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity.ReplaceViewBackRef(this);
        }
    }
}