using DELTation.LeoEcsExtensions.Components;
using Features.Progression.Services.Stages;
using Features.Progression.Views.Stages;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Stages
{
    public class StageNumberShowSystem : IEcsInitSystem
    {
        private readonly EcsFilter<ViewBackRef<StageNumberView>> _filter = default;
        private readonly IRuntimeStage _runtimeStage;

        public StageNumberShowSystem(IRuntimeStage runtimeStage) => _runtimeStage = runtimeStage;

        public void Init()
        {
            foreach (var i in _filter)
            {
                StageNumberView view = _filter.Get1(i);
                view.Show(StageIndex);
            }
        }

        private int StageIndex => _runtimeStage.Index;
    }
}