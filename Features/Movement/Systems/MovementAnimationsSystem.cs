using DELTation.LeoEcsExtensions.Components;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Systems
{
    public class MovementAnimationsSystem : IEcsRunSystem
    {
        private static readonly int SpeedId = Animator.StringToHash("Speed");
        private static readonly int IsMovingId = Animator.StringToHash("IsMoving");
        private readonly EcsFilter<UnityObjectData<Animator>, MovementData> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                Animator animator = _filter.Get1(i);
                ref var movementData = ref _filter.Get2(i);
                var actualMovementSpeed = movementData.Direction.magnitude * movementData.Speed;
                animator.SetFloat(SpeedId, actualMovementSpeed);
                animator.SetBool(IsMovingId, movementData.IsMoving);
            }
        }
    }
}