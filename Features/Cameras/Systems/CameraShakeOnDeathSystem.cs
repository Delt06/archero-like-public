using Features.Cameras.Components;
using Features.Health.Components;
using Leopotam.Ecs;

namespace Features.Cameras.Systems
{
    public class CameraShakeOnDeathSystem : IEcsRunSystem
    {
        private readonly EcsFilter<DeathEvent> _filter = default;
        private readonly EcsWorld _world = new EcsWorld();

        public void Run()
        {
            foreach (var _ in _filter)
            {
                _world.NewEntity().Get<CameraShakeCommand>();
                break;
            }
        }
    }
}