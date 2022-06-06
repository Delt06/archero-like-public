using DELTation.LeoEcsExtensions.Views;
using Features.Movement.Behaviours;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Physics.Behaviours
{
    [RequireComponent(typeof(IEntityView))]
    public class ZigZagCollisionEventGenerator : MonoBehaviour
    {
        private void OnControllerColliderHit(ControllerColliderHit hit)
        {
            if (!hit.collider.TryGetComponent(out ZigZagBounceTag _)) return;
            _normal = hit.normal;
        }

        private void LateUpdate()
        {
            if (_normal == null) return;
            if (!_entityView.TryGetEntity(out var entity)) return;
            ref var collisionEvent = ref entity.Get<ZigZagCollisionEvent>();
            collisionEvent.Normal = _normal.Value;
            _normal = null;
        }

        private void Awake()
        {
            _entityView = GetComponent<IEntityView>();
        }

        private IEntityView _entityView;
        private Vector3? _normal;
    }
}