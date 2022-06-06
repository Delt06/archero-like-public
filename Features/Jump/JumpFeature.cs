using DELTation.LeoEcsExtensions.Composition.Di;
using DELTation.LeoEcsExtensions.Features;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Jump.Components;
using Features.Jump.Systems;
using Leopotam.Ecs;

namespace Features.Jump
{
    public class JumpFeature : Feature
    {
        public override void Register(EcsSystems systems, EcsSystems physicsSystems)
        {
            systems
                .CreateAndAdd<JumpTimeSystem>()
                .CreateAndAdd<JumpCommandCreationSystem>()
                .CreateAndAdd<JumpCommandHandlingSystem>()
                .OneFrame<JumpCommand>()
                .CreateAndAdd<ActiveJumpTimeSystem>()
                .ReadFromTransforms()
                .CreateAndAdd<JumpEndParticleEffectSystem>()
                .CreateAndAdd<JumpEndExplosionSystem>()
                .CreateAndAdd<JumpAnimationSystem>()
                .OneFrame<JumpEvent>()
                .OneFrame<JumpEndEvent>()
                ;
        }
    }
}