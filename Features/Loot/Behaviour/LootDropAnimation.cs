using DG.Tweening;
using DOTweenExtensions;
using Sirenix.OdinInspector;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Features.Loot.Behaviour
{
    public class LootDropAnimation : MonoBehaviour
    {
        [SerializeField] [Required] private Transform _animatedObject = default;
        [SerializeField] [Min(0f)] private float _speed = 1f;
        [SerializeField] [MinMaxSlider(0f, 5f, true)]
        private Vector2 _jumpHeightRange = new Vector2(0.5f, 1f);
        [SerializeField] private Ease _easeUp = Ease.OutQuad;
        [SerializeField] private Ease _easeDown = Ease.OutBounce;

        private void OnEnable()
        {
            _animatedObject.localPosition = _initialLocalPosition;
            _sequence.Kill();
            var height = Random.Range(_jumpHeightRange.x, _jumpHeightRange.y);
            var halfDuration = height / _speed;
            _sequence.GetOrCreateSequence()
                .Append(_animatedObject.DOMoveY(_initialLocalPosition.y + height, halfDuration).SetEase(_easeUp))
                .Append(_animatedObject.DOMoveY(_initialLocalPosition.y, halfDuration).SetEase(_easeDown));
        }

        private void OnDisable()
        {
            _sequence.Kill();
        }

        private void Awake()
        {
            _initialLocalPosition = _animatedObject.localPosition;
        }

        private readonly ControlledSequence _sequence = new ControlledSequence();
        private Vector3 _initialLocalPosition;
    }
}