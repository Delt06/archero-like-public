using Features.Health.Components;
using Features.Jump.Components;
using Features.TimeUpdate.Services;
using Leopotam.Ecs;

namespace Features.Jump.Systems
{
    public class JumpTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<JumpingData>.Exclude<DeathTag> _filter = default;
        private readonly ITime _time;

        public JumpTimeSystem(ITime time) => _time = time;

        public void Run()
        {
            var deltaTime = _time.DeltaTime;

            foreach (var i in _filter)
            {
                ref var jumpingData = ref _filter.Get1(i);
                jumpingData.TimeToNextJump -= deltaTime;
            }
        }
    }
}