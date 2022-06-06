using System.Collections.Generic;
using Features.Progression.Assets.Upgrades;
using JetBrains.Annotations;
using Leopotam.Ecs;

namespace Features.Progression.Services.Upgrades
{
    public interface IUpgradeGenerator
    {
        void AddSystems(EcsSystems systems);
        void PopulateWithDistinctUpgrades([NotNull] List<UpgradeAsset> upgrades);
    }
}