using Features.Progression.Components;
using Features.Progression.Services.Stages;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Stages
{
    public class PassStageSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnteredStageGateEvent> _filter = default;
        private readonly IRuntimeStage _runtimeStage;

        public PassStageSystem(IRuntimeStage runtimeStage) => _runtimeStage = runtimeStage;

        public void Run()
        {
            foreach (var _ in _filter)
            {
                _runtimeStage.Pass();
                break;
            }
        }
    }
}