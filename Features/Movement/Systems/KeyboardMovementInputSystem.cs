using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Systems
{
    public class KeyboardMovementInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovementData, KeyboardMovementInputData> _filter = default;

        public void Run()
        {
            var direction = new Vector3(Input.GetAxisRaw("Horizontal"), 0f, Input.GetAxisRaw("Vertical"));
            direction = Vector3.ClampMagnitude(direction, 1f);

            foreach (var i in _filter)
            {
                ref var movementData = ref _filter.Get1(i);
                movementData.Direction = direction;
            }
        }
    }
}