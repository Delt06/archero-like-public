using DELTation.LeoEcsExtensions.Components;
using Features.Progression.Components.Upgrades;
using Features.Progression.Views.Upgrades;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Upgrades
{
    public class ShowUpgradesInfoSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AppliedUpgradeEvent> _eventFilter = default;
        private readonly EcsFilter<ViewBackRef<AppliedUpgradeInfoView>> _viewFilter = default;

        public void Run()
        {
            foreach (var iEvent in _eventFilter)
            {
                var info = _eventFilter.Get1(iEvent).UpgradeInfo;

                foreach (var iView in _viewFilter)
                {
                    AppliedUpgradeInfoView view = _viewFilter.Get1(iView);
                    view.Show(info);
                    break;
                }

                break;
            }
        }
    }
}