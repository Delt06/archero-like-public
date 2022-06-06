using DG.Tweening;
using UnityEngine;

namespace DOTweenExtensions
{
    public class TweenersCount : MonoBehaviour
    {
        [SerializeField] [Min(1)] private int _sequences = 100;
        [SerializeField] [Min(1)] private int _tweeners = 500;

        private void Awake()
        {
            DOTween.SetTweensCapacity(_tweeners, _sequences);
        }
    }
}