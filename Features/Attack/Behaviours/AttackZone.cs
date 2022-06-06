using System.Collections.Generic;
using DELTation.LeoEcsExtensions.Views;
using Features.Attack.Components;
using Features.Attack.Utilities;
using Features.Health.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Attack.Behaviours
{
    public class AttackZone : MonoBehaviour
    {
        [SerializeField] private Hand _hand = Hand.Right;

        public void Construct(IEntityView thisView)
        {
            _thisView = thisView;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (TryGetAttackableEntity(other, out var entity) && TryGetEntitiesInside(out var thisSide, out _))
                thisSide.Add(entity);
        }

        private void OnTriggerExit(Collider other)
        {
            if (TryGetAttackableEntity(other, out var entity) && TryGetEntitiesInside(out var thisSide, out _))
                thisSide.Remove(entity);
        }

        private void OnDisable()
        {
            if (TryGetEntitiesInside(out var thisSide, out var otherSide))
            {
                thisSide.Clear();
                otherSide.Clear();
            }
        }

        private static bool TryGetAttackableEntity(Collider other, out EcsEntity entity)
        {
            if (!other.TryGetComponent(out IEntityView view) ||
                !view.TryGetEntity(out var viewEntity) ||
                !viewEntity.Has<HealthData>())
            {
                entity = default;
                return false;
            }

            entity = viewEntity;
            return true;
        }

        private bool TryGetEntitiesInside(out HashSet<EcsEntity> thisSideEntities,
            out HashSet<EcsEntity> otherSideEntities)
        {
            thisSideEntities = default;
            otherSideEntities = default;
            if (!_thisView.TryGetEntity(out var entity)) return false;
            var attackZoneData = entity.Get<AttackZoneData>();
            thisSideEntities = attackZoneData.GetEntitiesInside(_hand);
            otherSideEntities = attackZoneData.GetEntitiesInside(_hand.GetOther());

            return true;
        }

        private IEntityView _thisView;
    }
}