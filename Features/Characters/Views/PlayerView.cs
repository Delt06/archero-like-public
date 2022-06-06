using DELTation.LeoEcsExtensions.Views;
using Leopotam.Ecs;

namespace Features.Characters.Views
{
    public class PlayerView : CharacterView
    {
        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity
                .ReplaceViewBackRef(this)
                ;
        }
    }
}