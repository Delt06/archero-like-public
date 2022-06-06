using DELTation.LeoEcsExtensions.Components;
using Features.AI.Components;
using Features.Health.Components;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Features.AI.Systems
{
    public class AgentMovementInputSystem : IEcsRunSystem
    {
        private readonly EcsFilter<MovementData, UnityObjectData<NavMeshAgent>, HealthData, CharacterAgentData>
            _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var healthData = ref _filter.Get3(i);
                if (!healthData.IsAlive) continue;

                ref var movementData = ref _filter.Get1(i);
                NavMeshAgent agent = _filter.Get2(i);
                var direction = agent.desiredVelocity;
                direction.y = 0f;
                direction = Vector3.ClampMagnitude(direction, 1f);
                movementData.Direction = direction;
            }
        }
    }
}