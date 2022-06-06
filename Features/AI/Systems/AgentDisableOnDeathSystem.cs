using DELTation.LeoEcsExtensions.Components;
using Features.AI.Components;
using Features.Health.Components;
using Leopotam.Ecs;
using UnityEngine.AI;

namespace Features.AI.Systems
{
    public class AgentDisableOnDeathSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnityObjectData<NavMeshAgent>, HealthData, CharacterAgentData> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var healthData = ref _filter.Get2(i);
                if (healthData.IsAlive) continue;

                NavMeshAgent agent = _filter.Get1(i);
                if (agent.enabled)
                {
                    agent.ResetPath();
                    agent.enabled = false;
                }
            }
        }
    }
}