using DELTation.LeoEcsExtensions.Utilities;
using Features.Jump.Components;
using Features.TimeUpdate.Services;
using Leopotam.Ecs;

namespace Features.Jump.Systems
{
    public class ActiveJumpTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ActiveJumpData> _filter = default;
        private readonly ITime _time;

        public ActiveJumpTimeSystem(ITime time) => _time = time;

        public void Run()
        {
            var deltaTime = _time.DeltaTime;

            foreach (var i in _filter)
            {
                ref var activeJumpData = ref _filter.Get1(i);
                activeJumpData.RemainingTime -= deltaTime;
                _filter.GetEntity(i).RequirePositionRead();
                if (activeJumpData.RemainingTime > 0f) continue;

                var entity = _filter.GetEntity(i);
                entity.Get<JumpEndEvent>();
                entity.Del<ActiveJumpData>();
            }
        }
    }
}