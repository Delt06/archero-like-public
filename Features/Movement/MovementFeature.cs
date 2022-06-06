using DELTation.LeoEcsExtensions.Composition.Di;
using DELTation.LeoEcsExtensions.Features;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Movement.Components;
using Features.Movement.Systems;
using Leopotam.Ecs;

namespace Features.Movement
{
    public class MovementFeature : Feature
    {
        public override void Register(EcsSystems systems, EcsSystems physicsSystems)
        {
            systems
                .CreateAndAdd<KeyboardMovementInputSystem>()
                .CreateAndAdd<JoystickMovementInputSystem>()
                .Add(new DeathResetSystem<MovementData>())
                .CreateAndAdd<DeadzoneMovementSystem>()
                .CreateAndAdd<ZigZagMovementSystem>()
                .OneFrame<ZigZagCollisionEvent>()
                .CreateAndAdd<StopMovementOnDeathSystem>()
                .CreateAndAdd<CharacterControllerMovementSystem>()
                .ReadFromTransforms()
                .CreateAndAdd<FindTargetSystem>()
                .CreateAndAdd<TargetRotationFromMovementDirectionSystem>()
                .CreateAndAdd<TargetRotationFromLookAtOverrideSystem>()
                .CreateAndAdd<RotateTowardsTargetRotationSystem>()
                .WriteToTransforms()
                .CreateAndAdd<MovementAnimationsSystem>()
                .CreateAndAdd<ExplosionSystem>()
                .OneFrame<ExplosionData>()
                ;
        }
    }
}