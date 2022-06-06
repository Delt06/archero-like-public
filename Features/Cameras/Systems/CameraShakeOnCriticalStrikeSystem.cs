using Features.Attack.Components;
using Features.Cameras.Components;
using Leopotam.Ecs;

namespace Features.Cameras.Systems
{
    public class CameraShakeOnCriticalStrikeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CriticalStrikeEvent> _filter = default;
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