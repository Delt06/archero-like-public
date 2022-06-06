using DELTation.LeoEcsExtensions.Components;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Systems
{
    public class ZigZagMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ZigZagMovementData, MovementData, Rotation> _startFilter = default;
        private readonly EcsFilter<ZigZagMovementData>.Exclude<ZigZagBounceReadyTag> _cooldownFilter = default;
        private readonly EcsFilter<ZigZagMovementData, MovementData, ZigZagCollisionEvent, ZigZagBounceReadyTag>
            _collisionFilter = default;

        public void Run()
        {
            TryAssignRandomDirection();
            UpdateCooldown(Time.deltaTime);
            HandleBouncing();
        }

        private void TryAssignRandomDirection()
        {
            foreach (var i in _startFilter)
            {
                ref var movementData = ref _startFilter.Get2(i);
                var directionSqrMagnitude = movementData.Direction.sqrMagnitude;
                if (!Mathf.Approximately(directionSqrMagnitude, 0f)) continue;

                ref var rotation = ref _startFilter.Get3(i);
                movementData.Direction = rotation.WorldRotation * Vector3.forward;
            }
        }

        private void UpdateCooldown(float deltaTime)
        {
            foreach (var i in _cooldownFilter)
            {
                ref var zigZagData = ref _cooldownFilter.Get1(i);
                zigZagData.BounceCooldownRemainingTime -= deltaTime;
                if (zigZagData.BounceCooldownRemainingTime <= 0f)
                    _cooldownFilter.GetEntity(i).Get<ZigZagBounceReadyTag>();
            }
        }

        private void HandleBouncing()
        {
            foreach (var i in _collisionFilter)
            {
                ref var zigZagData = ref _collisionFilter.Get1(i);
                ref var movementData = ref _collisionFilter.Get2(i);
                ref var collisionEvent = ref _collisionFilter.Get3(i);

                var bounceNormal = collisionEvent.Normal;
                bounceNormal.y = 0f;
                var minMagnitudeSqr = zigZagData.MinMagnitudeXZ * zigZagData.MinMagnitudeXZ;
                if (bounceNormal.sqrMagnitude < minMagnitudeSqr) continue;
                bounceNormal.Normalize();

                var newDirection = Vector3.Reflect(movementData.Direction, bounceNormal);
                movementData.Direction = newDirection;
                zigZagData.BounceCooldownRemainingTime = zigZagData.BounceCooldown;
                _collisionFilter.GetEntity(i).Del<ZigZagBounceReadyTag>();
            }
        }
    }
}