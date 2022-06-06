using System;
using Features.Characters;
using Features.Characters.Components;
using Features.Health.Components;
using Features.Progression.Components;
using Leopotam.Ecs;

namespace Features.Progression.Systems
{
    public class StageEndDetectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TeamData> _unitsFilter = default;
        private readonly EcsFilter<StageEndTag> _endTagsFilter = default;
        private readonly EcsWorld _world = default;

        public void Run()
        {
            if (IsEnded) return;
            if (AllEnemiesAreDeadAndAlliesAlive()) EndStage();
        }

        private bool IsEnded => _endTagsFilter.GetEntitiesCount() > 0;

        private bool AllEnemiesAreDeadAndAlliesAlive()
        {
            foreach (var i in _unitsFilter)
            {
                ref var teamData = ref _unitsFilter.Get1(i);
                var entity = _unitsFilter.GetEntity(i);

                switch (teamData.Team)
                {
                    case Team.Ally:
                        if (entity.Has<DeathTag>())
                            return false;
                        break;
                    case Team.Enemy:
                        if (!entity.Has<DeathTag>())
                            return false;
                        break;
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }

            return true;
        }

        private void EndStage()
        {
            var stageEndEntity = _world.NewEntity();
            stageEndEntity.Get<StageEndEvent>();
            stageEndEntity.Get<StageEndTag>();
        }
    }
}