using DELTation.LeoEcsExtensions.Composition.Di;
using DELTation.LeoEcsExtensions.Features;
using Features.Attack.Components;
using Features.Attack.Systems;
using Features.Health.Components;
using Features.Health.Systems;
using Features.PopUpText.Systems;
using Leopotam.Ecs;

namespace Features.Health
{
    public class HealthFeature : Feature
    {
        public override void Register(EcsSystems systems, EcsSystems physicsSystems)
        {
            systems
                .CreateAndAdd<DeathSystem>()
                .CreateAndAdd<CharacterControllerDeathToggleSystem>()
                .CreateAndAdd<DeathAnimationSystem>()
                .CreateAndAdd<RemoveTakeDamageCommandsIfStageEndedSystem>()
                .CreateAndAdd<TakeDamageSystem>()
                .OneFrame<TakeDamageCommand>()
                .CreateAndAdd<TakeDamageAnimationSystem>()
                .CreateAndAdd<TakeDamagePopUpTextSystem>()
                .CreateAndAdd<DisposePopUpTextSystem>()
                .CreateAndAdd<HitParticleEffectSystem>()
                .CreateAndAdd<HealingCommandProcessingSystem>()
                .OneFrame<HealingCommand>()
                .CreateAndAdd<CreateHealingPopUpTextSystem>()
                .OneFrame<HealingEvent>()
                .OneFrame<TakeDamageEvent>()
                .CreateAndAdd<UpdateHealthBarSystem>()
                .OneFrame<SnapHealthCommand>()
                ;
        }
    }
}