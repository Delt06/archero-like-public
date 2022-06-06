using DELTation.LeoEcsExtensions.Views;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Progression.Behaviours.Stages
{
    public class SpawnPoint : MonoBehaviour, IStageConstructionHandler
    {
        [SerializeField] [Required] private EntityView _prefab = default;

        void IStageConstructionHandler.OnConstruct()
        {
            Instantiate(_prefab, transform.position, transform.rotation);
        }
    }
}