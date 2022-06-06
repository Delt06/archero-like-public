using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Systems
{
    public class TargetRotationFromLookAtOverrideSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Position, RotationData, AttackTarget> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var currentPosition = _filter.Get1(i).WorldPosition;

                ref var lookAtOverride = ref _filter.Get3(i);
                var target = lookAtOverride.Target;
                if (!target.TryGet(out Position targetPosition)) continue;

                var lookAtPoint = targetPosition.WorldPosition;
                var offsetTowardsLookAt = lookAtPoint - currentPosition;
                const float distanceThreshold = 0.1f;

                ref var rotationData = ref _filter.Get2(i);

                if (offsetTowardsLookAt.sqrMagnitude < distanceThreshold * distanceThreshold)
                {
                    rotationData.HasTargetRotation = false;
                    continue;
                }

                rotationData.HasTargetRotation = true;
                var lookAtDirection = offsetTowardsLookAt.normalized;
                var targetRotation = Quaternion.LookRotation(lookAtDirection, Vector3.up);
                rotationData.TargetRotation = targetRotation;
            }
        }
    }
}