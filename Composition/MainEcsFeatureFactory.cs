using DELTation.LeoEcsExtensions.Composition;
using DELTation.LeoEcsExtensions.Composition.Di;
using Features.AI;
using Features.Attack;
using Features.Attack.Components;
using Features.Cameras;
using Features.Coins;
using Features.Health;
using Features.Health.Components;
using Features.Jump;
using Features.Loot;
using Features.Movement;
using Features.PopUpText;
using Features.Progression;
using Features.Progression.Components;
using Features.TimeUpdate;
using Features.UI;

namespace Composition
{
    public sealed class MainEcsFeatureFactory : EcsFeatureFactoryBehaviour
    {
        protected override void ConfigureFeatures(FeatureFactoryBuilder builder)
        {
            builder
                .CreateAndAdd<TimeUpdateFeature>()
                .CreateAndAdd<AiFeature>()
                .CreateAndAdd<JumpFeature>()
                .CreateAndAdd<MovementFeature>()
                .CreateAndAdd<AttackFeature>()
                .CreateAndAdd<HealthFeature>()
                .CreateAndAdd<LootFeature>()
                .CreateAndAdd<ProgressionFeature>()
                .CreateAndAdd<CoinsFeature>()
                .CreateAndAdd<CamerasFeature>()
                .CreateAndAdd<PopUpTextFeature>()
                .CreateAndAdd<UIFeature>()
                .OneFrame<ResetSessionDataCommand>()
                .OneFrame<DeathEvent>()
                .OneFrame<CriticalStrikeEvent>()
                .OneFrame<CriticalAttackTag>()
                ;
        }
    }
}