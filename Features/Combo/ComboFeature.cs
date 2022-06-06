using DELTation.LeoEcsExtensions.Composition.Di;
using DELTation.LeoEcsExtensions.Features;
using Features.Combo.Components;
using Features.Combo.Components.Effects;
using Features.Combo.Systems;
using Features.Combo.Systems.Effects;
using Leopotam.Ecs;

namespace Features.Combo
{
    public class ComboFeature : Feature
    {
        public override void Register(EcsSystems systems, EcsSystems physicsSystems)
        {
            systems
                .CreateAndAdd<ResetComboTimerOnMissSystem>()
                .CreateAndAdd<ComboTimerSystem>()
                .CreateAndAdd<IncreaseComboOnAttackSystem>()
                .CreateAndAdd<ComboDamageSystem>()
                .CreateAndAdd<CreateComboPopUpTextSystem>()
                ;

            systems
                .Add(new CreateComboEffectCommandSystem<ComboExplosionEffectData>())
                .CreateAndAdd<HandleExplosionComboEffectCommandSystem>()
                .CreateAndAdd<PlayExplosionComboParticleEffect>()
                .OneFrame<ComboEffectCommand<ComboExplosionEffectData>>()
                ;

            systems
                .Add(new CreateComboEffectCommandSystem<ComboBurningEffectData>())
                .CreateAndAdd<HandleBurningComboEffectCommandSystem>()
                .OneFrame<ComboEffectCommand<ComboBurningEffectData>>()
                ;

            systems
                .OneFrame<ComboHitEvent>()
                ;
        }
    }
}