using DELTation.LeoEcsExtensions.Components;
using Features.Progression.Components;
using Features.Progression.Views;
using Leopotam.Ecs;

namespace Features.Progression.Systems
{
    public class OpenStageGateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ViewBackRef<StageGateView>> _gatesFilter = default;
        private readonly EcsFilter<StageEndEvent> _eventsFilter = default;

        public void Run()
        {
            if (_eventsFilter.GetEntitiesCount() == 0) return;

            foreach (var i in _gatesFilter)
            {
                StageGateView gateView = _gatesFilter.Get1(i);
                gateView.Open();
            }
        }
    }
}