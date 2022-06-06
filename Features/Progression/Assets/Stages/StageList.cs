using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Progression.Assets.Stages
{
    [CreateAssetMenu]
    public class StageList : ScriptableObject
    {
        [SerializeField] [Required] [ListDrawerSettings(ShowIndexLabels = true)]
        private GameObject[] _stages = default;

        public GameObject[] Stages => _stages;
    }
}