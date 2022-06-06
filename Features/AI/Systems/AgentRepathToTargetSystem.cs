using System.Runtime.CompilerServices;
using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Features.AI.Components;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Features.AI.Systems
{
    public class AgentRepathToTargetSystem : IEcsRunSystem
    {
        private readonly
            EcsFilter<UnityObjectData<NavMeshAgent>, CharacterAgentData, AttackTarget, Position,
                AgentTowardsTargetMovementTag>
            _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var agentData = ref _filter.Get2(i);
                agentData.TimeBeforeRepath -= Time.deltaTime;
                if (agentData.TimeBeforeRepath > 0f) continue;

                NavMeshAgent agent = _filter.Get1(i);
                if (!agent.IsReadyForPath()) continue;

                var currentPosition = _filter.Get4(i).WorldPosition;
                var target = _filter.Get3(i).Target;
                if (!target.TryGet(out Position targetPosition)) continue;
                var destination = targetPosition.WorldPosition;
                var offsetFromTarget = GetOffsetFromTarget(currentPosition, destination, agentData.DesiredDistance);
                destination += offsetFromTarget;

                var sqrDistance = (currentPosition - destination).sqrMagnitude;
                if (sqrDistance <= agentData.StoppingDistance * agentData.StoppingDistance) continue;

                if (agent.SetDestination(destination))
                    agentData.TimeBeforeRepath = Random.Range(agentData.RepathMinInterval, agentData.RepathMaxInterval);
            }
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        private static Vector3 GetOffsetFromTarget(Vector3 currentPosition, Vector3 destination, float desiredDistance)
        {
            var directionTowardsCurrentPosition = (currentPosition - destination).normalized;
            var offsetFromTarget = directionTowardsCurrentPosition * desiredDistance;
            return offsetFromTarget;
        }
    }
}