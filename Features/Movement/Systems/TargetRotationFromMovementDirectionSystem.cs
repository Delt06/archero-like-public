using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Systems
{
    public class TargetRotationFromMovementDirectionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovementData, RotationData>.Exclude<AttackTarget> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var movementData = ref _filter.Get1(i);
                ref var rotationData = ref _filter.Get2(i);
                if (!movementData.IsMoving)
                {
                    rotationData.HasTargetRotation = false;
                    continue;
                }

                rotationData.HasTargetRotation = true;
                var targetRotation = Quaternion.LookRotation(movementData.Direction, Vector3.up);
                rotationData.TargetRotation = targetRotation;
            }
        }
    }
}