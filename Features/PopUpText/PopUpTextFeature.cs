using DELTation.LeoEcsExtensions.Composition.Di;
using DELTation.LeoEcsExtensions.Features;
using Features.PopUpText.Systems;
using Leopotam.Ecs;

namespace Features.PopUpText
{
    public class PopUpTextFeature : Feature
    {
        public override void Register(EcsSystems systems, EcsSystems physicsSystems)
        {
            systems.CreateAndAdd<DisposePopUpTextSystem>();
        }
    }
}