using Features.Attack.Components;
using Features.Progression.Components;
using Leopotam.Ecs;

namespace Features.Attack.Systems
{
    public class RemoveTakeDamageCommandsIfStageEndedSystem : IEcsRunSystem
    {
        private readonly EcsFilter<StageEndTag> _stageEndFilter = default;
        private readonly EcsFilter<TakeDamageCommand> _takeDamageFilter = default;

        public void Run()
        {
            if (_stageEndFilter.GetEntitiesCount() == 0) return;

            foreach (var i in _takeDamageFilter)
            {
                var entity = _takeDamageFilter.GetEntity(i);
                entity.Del<TakeDamageCommand>();
            }
        }
    }
}