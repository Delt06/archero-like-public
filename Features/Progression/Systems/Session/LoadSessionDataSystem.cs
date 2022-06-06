using Features.Characters.Components;
using Features.Coins.Components;
using Features.Health.Components;
using Features.Progression.Services.Session;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Session
{
    public class LoadSessionDataSystem : IEcsInitSystem
    {
        private readonly EcsFilter<PlayerTag> _playerFilter = default;
        private readonly ISessionProgress _sessionProgress;

        public LoadSessionDataSystem(ISessionProgress sessionProgress) => _sessionProgress = sessionProgress;

        public void Init()
        {
            foreach (var i in _playerFilter)
            {
                var entity = _playerFilter.GetEntity(i);
                _sessionProgress.LoadInto(entity);

                entity.Get<SnapHealthCommand>();
                entity.Get<CoinsDataChangeEvent>();
            }
        }
    }
}