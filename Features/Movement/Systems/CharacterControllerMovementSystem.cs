using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Systems
{
    public class CharacterControllerMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovementData, UnityObjectData<CharacterController>> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var movementData = ref _filter.Get1(i);
                if (!movementData.IsMoving) continue;

                CharacterController characterController = _filter.Get2(i);
                if (!characterController.enabled) continue;

                var motion = movementData.Speed * Time.deltaTime;
                var motionVector = movementData.Direction * motion;
                characterController.Move(motionVector);
                _filter.GetEntity(i).RequirePositionRead();
            }
        }
    }
}