using Features.Progression.Services.Stages;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Features.Progression.Behaviours.Stages
{
    public class StageSceneLoader : MonoBehaviour, IStageSceneLoader
    {
        [SerializeField] [Min(1)] private int _index = 1;

        public void Load() => SceneManager.LoadScene(_index);
    }
}