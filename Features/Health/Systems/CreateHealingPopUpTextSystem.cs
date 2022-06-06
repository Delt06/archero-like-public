using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Pooling;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Health.Components;
using Features.PopUpText.Assets;
using Features.PopUpText.Behaviours.Pooling;
using Features.PopUpText.Views;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Health.Systems
{
    public class CreateHealingPopUpTextSystem : IEcsRunSystem
    {
        private readonly EcsFilter<HealingEvent> _events = default;
        private readonly IEntityViewPool<PopUpTextView> _pool;
        private readonly PopUpTextDataAsset _popUpTextData;

        public CreateHealingPopUpTextSystem(IEntityViewPool<PopUpTextView> pool, PopUpTextDataAsset popUpTextData)
        {
            _pool = pool;
            _popUpTextData = popUpTextData;
        }

        public void Run()
        {
            foreach (var i in _events)
            {
                ref var @event = ref _events.Get1(i);
                var target = @event.Target;
                if (!target.TryGet(out Position position)) continue;

                ShowPopup(@event.RestoredHealth, position.WorldPosition);
            }
        }

        private void ShowPopup(float restoredHealth, Vector3 rootPosition)
        {
            var view = _pool.Create(rootPosition, Quaternion.identity);
            var displayedDamage = Mathf.Ceil(restoredHealth);
            view.Show("+{0:0}", displayedDamage, _popUpTextData.HealingTextColor, _popUpTextData.HealingTextDuration);
        }
    }
}