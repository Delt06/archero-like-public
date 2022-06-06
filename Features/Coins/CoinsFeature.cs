using DELTation.LeoEcsExtensions.Composition.Di;
using DELTation.LeoEcsExtensions.Features;
using Features.Coins.Components;
using Features.Coins.Systems;
using Leopotam.Ecs;

namespace Features.Coins
{
    public class CoinsFeature : Feature
    {
        public override void Register(EcsSystems systems, EcsSystems physicsSystems)
        {
            systems
                .CreateAndAdd<CoinsCountUpdateSystem>()
                .OneFrame<CoinsDataChangeEvent>();
        }
    }
}