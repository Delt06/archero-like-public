using System.Collections.Generic;
using DELTation.LeoEcsExtensions.Components;
using Features.Progression.Assets.Upgrades;
using Features.Progression.Components;
using Features.Progression.Services.Upgrades;
using Features.Progression.Views.Upgrades;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Upgrades
{
    public class ShowUpgradeScreenSystem : IEcsRunSystem
    {
        private readonly EcsFilter<LevelUpEvent> _eventFilter = default;
        private readonly EcsFilter<ViewBackRef<UpgradeChoiceScreenView>> _viewFilter = default;
        private readonly IUpgradeGenerator _upgradeGenerator;
        private readonly List<UpgradeAsset> _upgrades = new List<UpgradeAsset>();

        public ShowUpgradeScreenSystem(IUpgradeGenerator upgradeGenerator) => _upgradeGenerator = upgradeGenerator;

        public void Run()
        {
            foreach (var _ in _eventFilter)
            {
                foreach (var i in _viewFilter)
                {
                    UpgradeChoiceScreenView view = _viewFilter.Get1(i);
                    _upgradeGenerator.PopulateWithDistinctUpgrades(_upgrades);
                    view.Show(_upgrades);
                    _upgrades.Clear();
                }

                break;
            }
        }
    }
}