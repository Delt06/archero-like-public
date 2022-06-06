using Features.Progression.Components;
using Features.Progression.Services.Session;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Session
{
    public class ResetSessionDataSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ResetSessionDataCommand> _filter = default;
        private readonly ISessionProgress _sessionProgress;

        public ResetSessionDataSystem(ISessionProgress sessionProgress) => _sessionProgress = sessionProgress;

        public void Run()
        {
            foreach (var _ in _filter)
            {
                _sessionProgress.ResetAll();
                break;
            }
        }
    }
}