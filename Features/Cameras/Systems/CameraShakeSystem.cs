using DELTation.LeoEcsExtensions.Components;
using Features.Cameras.Components;
using Features.Cameras.Views;
using Leopotam.Ecs;

namespace Features.Cameras.Systems
{
    public class CameraShakeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<CameraShakeCommand> _commandsFilter = default;
        private readonly EcsFilter<ViewBackRef<CameraShakeView>> _viewsFilter = default;

        public void Run()
        {
            foreach (var _ in _commandsFilter)
            {
                foreach (var iView in _viewsFilter)
                {
                    CameraShakeView view = _viewsFilter.Get1(iView);
                    view.Shake();
                }

                break;
            }
        }
    }
}