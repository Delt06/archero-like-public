using DELTation.LeoEcsExtensions.Components;
using Features.Characters.Components;
using Features.Health.Components;
using Features.UI.Views;
using Leopotam.Ecs;

namespace Features.UI.Systems
{
    public class ShowLoseScreenSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, DeathEvent> _playerDeathFilter = default;
        private readonly EcsFilter<ViewBackRef<LoseScreenView>> _viewsFilter = default;

        public void Run()
        {
            foreach (var _ in _playerDeathFilter)
            {
                foreach (var i in _viewsFilter)
                {
                    LoseScreenView view = _viewsFilter.Get1(i);
                    view.Open();
                }

                break;
            }
        }
    }
}