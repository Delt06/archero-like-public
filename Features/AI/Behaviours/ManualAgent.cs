using UnityEngine;
using UnityEngine.AI;

namespace Features.AI.Behaviours
{
    public class ManualAgent : MonoBehaviour
    {
        private void Awake()
        {
            _agent = GetComponent<NavMeshAgent>();
            _agent.updatePosition = false;
            _agent.updateRotation = false;
            _agent.updateUpAxis = false;
        }

        private NavMeshAgent _agent;
    }
}