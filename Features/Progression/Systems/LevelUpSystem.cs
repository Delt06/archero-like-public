using Features.Progression.Components;
using Features.Progression.Services;
using Leopotam.Ecs;

namespace Features.Progression.Systems
{
    public class LevelUpSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ExperienceData, ExperienceIncreaseEvent> _filter = default;
        private readonly EcsWorld _world = default;
        private readonly ILevelRequirementProvider _requirementProvider;

        public LevelUpSystem(ILevelRequirementProvider requirementProvider) =>
            _requirementProvider = requirementProvider;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var experienceData = ref _filter.Get1(i);
                var entity = _filter.GetEntity(i);

                while (true)
                {
                    var requirement = _requirementProvider.GetExperienceFor(experienceData.Level + 1);
                    if (experienceData.Experience < requirement) break;

                    experienceData.Experience -= requirement;
                    experienceData.Level++;
                    ref var levelUpEvent = ref _world.NewEntity().Get<LevelUpEvent>();
                    levelUpEvent.Entity = entity;
                }
            }
        }
    }
}