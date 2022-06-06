using DELTation.LeoEcsExtensions.Composition.Di;
using DELTation.LeoEcsExtensions.Features;
using Features.Loot.Systems.Collect;
using Features.Progression.Components;
using Features.Progression.Components.Upgrades;
using Features.Progression.Services.Upgrades;
using Features.Progression.Systems;
using Features.Progression.Systems.Session;
using Features.Progression.Systems.Stages;
using Features.Progression.Systems.Upgrades;
using Leopotam.Ecs;

namespace Features.Progression
{
    public class ProgressionFeature : Feature
    {
        private readonly IUpgradeGenerator _upgradeGenerator;

        public ProgressionFeature(IUpgradeGenerator upgradeGenerator) => _upgradeGenerator = upgradeGenerator;

        public override void Register(EcsSystems systems, EcsSystems physicsSystems)
        {
            systems.CreateAndAdd<StageNumberShowSystem>();

            systems
                .CreateAndAdd<LoadSessionDataSystem>()
                .CreateAndAdd<StageEndDetectionSystem>()
                .CreateAndAdd<ResetSessionDataSystem>()
                ;
            RegisterExperience(systems);
            RegisterUpgrades(systems);
            systems
                .CreateAndAdd<OpenStageGateSystem>()
                .CreateAndAdd<EnterStageGateSystem>()
                .CreateAndAdd<SaveSessionDataSystem>()
                .CreateAndAdd<PassStageSystem>()
                .CreateAndAdd<CreateSceneLoadCommandOnEnterStageGateSystem>()
                .CreateAndAdd<StageSceneLoadSystem>()
                .OneFrame<StageEndEvent>()
                .OneFrame<ExperienceIncreaseEvent>()
                .OneFrame<LevelUpEvent>()
                .OneFrame<EnteredStageGateEvent>()
                .OneFrame<StageSceneLoadCommand>()
                ;
        }

        private static void RegisterExperience(EcsSystems systems)
        {
            systems
                .CreateAndAdd<CollectExperienceLootSystem>()
                .CreateAndAdd<LevelUpSystem>()
                .CreateAndAdd<ExperienceBarSystem>()
                ;
        }

        private void RegisterUpgrades(EcsSystems systems)
        {
            systems.CreateAndAdd<ShowUpgradeScreenSystem>();
            _upgradeGenerator.AddSystems(systems);
            systems
                .CreateAndAdd<ShowUpgradesInfoSystem>()
                ;
            systems
                .OneFrame<UpgradeInfoData>()
                .OneFrame<AppliedUpgradeEvent>()
                ;
        }
    }
}