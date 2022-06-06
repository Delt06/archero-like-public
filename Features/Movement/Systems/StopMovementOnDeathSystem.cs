using Features.Health.Components;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Systems
{
    public class StopMovementOnDeathSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovementData, DeathTag> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var movementData = ref _filter.Get1(i);
                movementData.Direction = Vector3.zero;
                movementData.IsMoving = false;
            }
        }
    }
}