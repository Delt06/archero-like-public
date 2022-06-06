using DELTation.LeoEcsExtensions.Composition.Di;
using DELTation.LeoEcsExtensions.Features;
using Features.AI.Systems;
using Leopotam.Ecs;

namespace Features.AI
{
    public class AiFeature : Feature
    {
        public override void Register(EcsSystems systems, EcsSystems physicsSystems)
        {
            systems
                .CreateAndAdd<AgentRepathToTargetSystem>()
                .CreateAndAdd<AgentMovementInputSystem>()
                .CreateAndAdd<AgentSnapSystem>()
                .CreateAndAdd<AgentStopWhenCloseSystem>()
                .CreateAndAdd<AgentDisableOnDeathSystem>()
                ;
        }
    }
}