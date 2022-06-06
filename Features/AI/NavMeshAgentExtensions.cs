using UnityEngine.AI;

namespace Features.AI
{
    public static class NavMeshAgentExtensions
    {
        public static bool IsReadyForPath(this NavMeshAgent agent) =>
            agent.enabled && agent.isOnNavMesh &&
            !agent.hasPath && !agent.pathPending;
    }
}