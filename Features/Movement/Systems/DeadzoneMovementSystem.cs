using Features.Movement.Components;
using Leopotam.Ecs;

namespace Features.Movement.Systems
{
    public class DeadzoneMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovementData, MovementDeadzoneData> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var movementData = ref _filter.Get1(i);
                var minValue = _filter.Get2(i).MinValue;
                var direction = movementData.Direction;
                movementData.IsMoving = direction.sqrMagnitude >= minValue * minValue;
            }
        }
    }
}