using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Systems
{
    public class RotateTowardsTargetRotationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Rotation, RotationData> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var rotationData = ref _filter.Get2(i);
                if (!rotationData.HasTargetRotation) continue;

                ref var rotation = ref _filter.Get1(i);
                var currentRotation = rotation.WorldRotation;

                var targetRotationEulerAngles = rotationData.TargetRotation.eulerAngles;
                targetRotationEulerAngles.x = targetRotationEulerAngles.z = 0f;
                var targetRotation = Quaternion.Euler(targetRotationEulerAngles);

                var deltaAngle = rotationData.RotationSpeed * Time.deltaTime;
                var newRotation = Quaternion.RotateTowards(currentRotation, targetRotation, deltaAngle);
                _filter.GetEntity(i).UpdateRotation(ref rotation, newRotation);
            }
        }
    }
}