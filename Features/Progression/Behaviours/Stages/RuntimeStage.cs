using Features.Progression.Assets.Stages;
using Features.Progression.Services.Session;
using Features.Progression.Services.Stages;
using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.AI;

namespace Features.Progression.Behaviours.Stages
{
    public class RuntimeStage : MonoBehaviour, IRuntimeStage
    {
        [SerializeField] [Required] [AssetSelector]
        private StageList _stageList = default;

        [SerializeField] [Required] private GameObject _player = default;

        private ISessionProgress _sessionProgress;

        public void Construct(ISessionProgress sessionProgress)
        {
            _sessionProgress = sessionProgress;
        }

        public int Index { get; private set; }

        public void Pass() => _sessionProgress.OnPassedStage(Index);

        private void Awake()
        {
            Index = NextStageIndex;
            var stagePrefabIndex = Index % _stageList.Stages.Length;
            var stagePrefab = _stageList.Stages[stagePrefabIndex];
            var stage = Instantiate(stagePrefab);
            stage.GetComponent<NavMeshSurface>().BuildNavMesh();

            foreach (var handler in stage.GetComponentsInChildren<IStageConstructionHandler>())
            {
                handler.OnConstruct();
            }

            _player.SetActive(true);
        }

        private int NextStageIndex => _sessionProgress.LastPassedStageIndex.GetValueOrDefault(-1) + 1;
    }
}