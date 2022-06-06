using DELTation.LeoEcsExtensions.Components;
using Features.AI.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.AI;

namespace Features.AI.Systems
{
    public class AgentSnapSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnityObjectData<NavMeshAgent>, Position, CharacterAgentData>
            _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                const float maxDifference = 0.25f;
                NavMeshAgent agent = _filter.Get1(i);
                var actualPosition = _filter.Get2(i).WorldPosition;
                var sqrDifference = (actualPosition - agent.nextPosition).sqrMagnitude;
                if (sqrDifference > maxDifference * maxDifference)
                    agent.nextPosition = actualPosition;
            }
        }
    }
}