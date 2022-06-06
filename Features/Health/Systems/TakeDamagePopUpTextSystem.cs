using System.Collections.Generic;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Pooling;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Attack.Components;
using Features.Health.Components;
using Features.PopUpText.Assets;
using Features.PopUpText.Views;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Health.Systems
{
    public class TakeDamagePopUpTextSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TakeDamageEvent> _events = default;
        private readonly IEntityViewPool<PopUpTextView> _pool;
        private readonly PopUpTextDataAsset _popUpTextData;
        private readonly Dictionary<EcsEntity, (EcsEntity entity, TakeDamageEvent @event)> _tookDamage =
            new Dictionary<EcsEntity, (EcsEntity entity, TakeDamageEvent @event)>();

        public TakeDamagePopUpTextSystem(IEntityViewPool<PopUpTextView> pool, PopUpTextDataAsset popUpTextData)
        {
            _pool = pool;
            _popUpTextData = popUpTextData;
        }

        public void Run()
        {
            foreach (var i in _events)
            {
                ref var @event = ref _events.Get1(i);
                var eventTarget = @event.Target;
                if (_tookDamage.TryGetValue(eventTarget, out var existingEvent))
                {
                    var entity = _events.GetEntity(i);
                    if (entity.Has<CriticalAttackTag>())
                        existingEvent.entity = entity;
                    existingEvent.@event.Damage += @event.Damage;
                    _tookDamage[eventTarget] = existingEvent;
                }
                else
                {
                    _tookDamage[eventTarget] = (_events.GetEntity(i), @event);
                }
            }

            foreach (var kvp in _tookDamage)
            {
                var entity = kvp.Key;
                var color = entity.Has<CriticalAttackTag>()
                    ? _popUpTextData.CriticalDamageTextColor
                    : _popUpTextData.DamageTextColor;
                TryShowPopup(kvp.Value.@event, color);
            }

            _tookDamage.Clear();
        }

        private void TryShowPopup(in TakeDamageEvent @event, Color color)
        {
            var target = @event.Target;
            if (!target.TryGet(out Position position)) return;

            var rootPosition = position.WorldPosition;
            rootPosition += Random.Range(-1f, 1f) * _popUpTextData.DamageTextRandomOffset * Vector3.right;
            ShowPopup(@event.Damage, rootPosition, color);
        }

        private void ShowPopup(float damage, Vector3 rootPosition, Color color)
        {
            var view = _pool.Create(rootPosition, Quaternion.identity);
            var displayedDamage = Mathf.Ceil(damage);
            view.Show("-{0:0}", displayedDamage, color, _popUpTextData.DamageTextDuration);
        }
    }
}