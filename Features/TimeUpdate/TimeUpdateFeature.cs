using DELTation.LeoEcsExtensions.Composition.Di;
using DELTation.LeoEcsExtensions.Features;
using Features.TimeUpdate.Systems;
using Leopotam.Ecs;

namespace Features.TimeUpdate
{
    public class TimeUpdateFeature : Feature
    {
        public override void Register(EcsSystems systems, EcsSystems physicsSystems)
        {
            systems.CreateAndAdd<UpdateTimeSystem>();
        }
    }
}