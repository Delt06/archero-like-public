using Features.Characters.Components;
using Features.Progression.Components;
using Features.Progression.Services.Session;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Session
{
    public class SaveSessionDataSystem : IEcsRunSystem
    {
        private readonly EcsFilter<PlayerTag, EnteredStageGateTag> _playerFilter = default;
        private readonly ISessionProgress _sessionProgress;

        public SaveSessionDataSystem(ISessionProgress sessionProgress) => _sessionProgress = sessionProgress;

        public void Run()
        {
            foreach (var i in _playerFilter)
            {
                var entity = _playerFilter.GetEntity(i);
                _sessionProgress.SaveFrom(entity);
            }
        }
    }
}