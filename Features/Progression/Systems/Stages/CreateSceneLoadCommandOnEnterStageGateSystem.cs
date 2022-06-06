using Features.Progression.Components;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Stages
{
    public class CreateSceneLoadCommandOnEnterStageGateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<EnteredStageGateEvent> _filter = default;
        private readonly EcsWorld _world = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var commandEntity = _world.NewEntity();
                commandEntity.Get<StageSceneLoadCommand>();
                break;
            }
        }
    }
}