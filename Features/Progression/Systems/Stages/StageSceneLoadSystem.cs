using Features.Progression.Components;
using Features.Progression.Services.Stages;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Stages
{
    public class StageSceneLoadSystem : IEcsRunSystem
    {
        private readonly EcsFilter<StageSceneLoadCommand> _filter = default;
        private readonly IStageSceneLoader _sceneLoader;

        public StageSceneLoadSystem(IStageSceneLoader sceneLoader) => _sceneLoader = sceneLoader;

        public void Run()
        {
            foreach (var _ in _filter)
            {
                _sceneLoader.Load();
                break;
            }
        }
    }
}