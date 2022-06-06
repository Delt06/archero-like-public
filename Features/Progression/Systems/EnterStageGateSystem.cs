using Features.Characters;
using Features.Characters.Components;
using Features.Progression.Components;
using Leopotam.Ecs;

namespace Features.Progression.Systems
{
    public class EnterStageGateSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TeamData, EnteredStageGateEvent>.Exclude<EnteredStageGateTag> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var teamData = ref _filter.Get1(i);
                if (teamData.Team != Team.Ally)
                {
                    _filter.GetEntity(i).Del<EnteredStageGateEvent>();
                    continue;
                }

                var entity = _filter.GetEntity(i);
                entity.Get<EnteredStageGateTag>();
            }
        }
    }
}