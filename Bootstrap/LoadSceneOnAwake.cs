using UnityEngine;
using UnityEngine.SceneManagement;

namespace Bootstrap
{
    public class LoadSceneOnAwake : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int _index = 1;

        private void Awake()
        {
            SceneManager.LoadScene(_index);
        }
    }
}