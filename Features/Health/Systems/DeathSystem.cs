using Features.Health.Components;
using Leopotam.Ecs;

namespace Features.Health.Systems
{
    public class DeathSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealthData>.Exclude<DeathTag> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var healthData = ref _filter.Get1(i);
                if (healthData.Health <= 0f)
                {
                    var entity = _filter.GetEntity(i);
                    entity.Get<DeathTag>();
                    entity.Get<DeathEvent>();
                    healthData.IsAlive = false;
                }
            }
        }
    }
}