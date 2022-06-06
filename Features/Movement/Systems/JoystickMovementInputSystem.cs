using DELTation.LeoEcsExtensions.Components;
using Features.Movement.Components;
using Features.Movement.Views;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Systems
{
    public class JoystickMovementInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ViewBackRef<JoystickView>> _joystickFilter = default;
        private readonly EcsFilter<MovementData, JoystickMovementInputData> _movementFilter = default;

        public void Run()
        {
            foreach (var iJoystick in _joystickFilter)
            {
                JoystickView view = _joystickFilter.Get1(iJoystick);
                var value = view.Value;
                var direction = new Vector3(value.x, 0f, value.y);
                direction = Vector3.ClampMagnitude(direction, 1f);

                foreach (var iMovement in _movementFilter)
                {
                    ref var movementData = ref _movementFilter.Get1(iMovement);
                    movementData.Direction = direction;
                }

                break;
            }
        }
    }
}