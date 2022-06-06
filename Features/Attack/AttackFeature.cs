using DELTation.LeoEcsExtensions.Composition.Di;
using DELTation.LeoEcsExtensions.Features;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Attack.Components;
using Features.Attack.Components.Projectiles;
using Features.Attack.Systems;
using Features.Attack.Systems.Projectiles;
using Features.Attack.Systems.Steady;
using Features.Combo;
using Leopotam.Ecs;

namespace Features.Attack
{
    public class AttackFeature : Feature
    {
        public override void Register(EcsSystems systems, EcsSystems physicsSystems)
        {
            systems
                .CreateAndAdd<ProjectileMovementSystem>()
                .WriteToTransforms()
                .CreateAndAdd<ProjectileLifetimeSystem>()
                .CreateAndAdd<ProjectileDealDamageOnHitSystem>()
                .OneFrame<ProjectileHitEvent>()
                .OneFrame<ProjectileCreationEvent>()
                ;

            systems
                .CreateAndAdd<AutoAttackSystem>()
                .CreateAndAdd<BlockAttackWhenNotSteadySystem>()
                .CreateAndAdd<AttackStartSystem>()
                .CreateAndAdd<ForceFinishAttackOnDeathSystem>()
                .CreateAndAdd<ResetRangedDataOnAttackStartSystem>()
                .CreateAndAdd<AttackTimeSystem>()
                .CreateAndAdd<ShootProjectileSystem>()
                .CreateAndAdd<ProjectileDataPropagationSystem>()
                .CreateAndAdd<AttackZoneClearSystem>()
                .CreateAndAdd<MeleeAttackCommandSystem>()
                .CreateAndAdd<RemoveAttackCommandsWithDeadTargetsSystem>()
                .CreateAndAdd<CriticalAttackSystem>()
                ;

            (systems, physicsSystems)
                .CreateAndAddFeature<ComboFeature>()
                ;

            systems
                .CreateAndAdd<AttackCommandToTakeDamageSystem>()
                .OneFrame<AttackCommand>()
                .CreateAndAdd<BurningSystem>()
                .CreateAndAdd<PlayBurningParticleEffect>()
                .CreateAndAdd<AttackAnimationsSystem>()
                .OneFrame<AttackStartEvent>()
                .OneFrame<AttackFinishEvent>()
                ;

            systems
                .CreateAndAdd<ProjectileDestructionSystem>()
                .OneFrame<ProjectileDestructionCommand>()
                ;

            systems
                .CreateAndAdd<AttackTargetIndicatorSystem>();
        }
    }
}