using DELTation.LeoEcsExtensions.Composition.Di;
using DELTation.LeoEcsExtensions.Features;
using Features.UI.Systems;
using Leopotam.Ecs;

namespace Features.UI
{
    public class UIFeature : Feature
    {
        public override void Register(EcsSystems systems, EcsSystems physicsSystems)
        {
            systems
                .CreateAndAdd<ShowLoseScreenSystem>()
                ;
        }
    }
}