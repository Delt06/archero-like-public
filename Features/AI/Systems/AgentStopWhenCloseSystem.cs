using DELTation.LeoEcsExtensions.Components;
using Features.AI.Components;
using Leopotam.Ecs;
using UnityEngine.AI;

namespace Features.AI.Systems
{
    public class AgentStopWhenCloseSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnityObjectData<NavMeshAgent>, CharacterAgentData> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                NavMeshAgent agent = _filter.Get1(i);
                if (!agent.hasPath) continue;

                var destination = agent.destination;
                var sqrDifference = (agent.nextPosition - destination).sqrMagnitude;
                var agentData = _filter.Get2(i);
                if (sqrDifference <= agentData.StoppingDistance * agentData.StoppingDistance)
                    agent.ResetPath();
            }
        }
    }
}