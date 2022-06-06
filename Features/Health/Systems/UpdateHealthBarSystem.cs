using DELTation.LeoEcsExtensions.Components;
using Features.Health.Components;
using Features.Health.Views;
using Leopotam.Ecs;

namespace Features.Health.Systems
{
    public class UpdateHealthBarSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ViewBackRef<HealthBarView>> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                HealthBarView view = _filter.Get1(i);
                var snap = view.OwnerView.TryGetEntity(out var ownerEntity) && ownerEntity.Has<SnapHealthCommand>();
                view.UpdateBar(snap);
            }
        }
    }
}