using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Pooling;
using Features.PopUpText.Components;
using Features.PopUpText.Views;
using Leopotam.Ecs;

namespace Features.PopUpText.Systems
{
    public class DisposePopUpTextSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ViewBackRef<PopUpTextView>, PoolBackRef, PopUpAnimationEndedTag>
            _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                PopUpTextView view = _filter.Get1(i);
                EntityViewPool pool = _filter.Get2(i);
                pool.Dispose(view);
            }
        }
    }
}