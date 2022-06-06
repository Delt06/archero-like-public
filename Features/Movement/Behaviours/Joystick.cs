using Sirenix.OdinInspector;
using UnityEngine;
using UnityEngine.EventSystems;

namespace Features.Movement.Behaviours
{
    public class Joystick : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragHandler
    {
        [SerializeField] [Required] private RectTransform _background = default;
        [SerializeField] [Required] private RectTransform _handle = default;
        [SerializeField] [Required] private CanvasGroup _canvasGroup = default;
        [SerializeField] [Range(0f, 1f)] private float _idleAlpha = 0.5f;
        [SerializeField] [Range(0f, 1f)] private float _normalAlpha = 1f;

        private float _handleMaxDistance;
        private Vector2 _initialBackgroundPosition;
        private int? _pointerId;
        private Vector2 _pointerDownPosition;

        public Vector2 Value =>
            IsHeld ? _handle.anchoredPosition / _handleMaxDistance : Vector2.zero;

        public bool IsHeld => _pointerId.HasValue;

        public void OnPointerDown(PointerEventData eventData)
        {
            if (_pointerId != null) return;
            _pointerId = eventData.pointerId;
            _pointerDownPosition = eventData.position;
            _canvasGroup.alpha = _normalAlpha;

            var localPointerPosition = transform.InverseTransformPoint(_pointerDownPosition);
            _background.localPosition = localPointerPosition;
        }

        public void OnPointerUp(PointerEventData eventData)
        {
            if (_pointerId != eventData.pointerId) return;
            _pointerId = null;
            ResetBackground();
            ResetHandle();
        }

        public void OnDrag(PointerEventData eventData)
        {
            if (_pointerId != eventData.pointerId) return;

            var localPointerPosition = transform.InverseTransformPoint(eventData.position);
            var offsetFromBackground = localPointerPosition - _background.localPosition;
            offsetFromBackground = Vector2.ClampMagnitude(offsetFromBackground, _handleMaxDistance);
            _handle.anchoredPosition = offsetFromBackground;
        }

        private void Awake()
        {
            _handleMaxDistance = _handle.anchoredPosition.magnitude;
            _initialBackgroundPosition = _background.anchoredPosition;
            ResetBackground();
            ResetHandle();
        }

        private void ResetBackground()
        {
            _canvasGroup.alpha = _idleAlpha;
            _background.anchoredPosition = _initialBackgroundPosition;
        }

        private void ResetHandle()
        {
            _handle.anchoredPosition = Vector2.zero;
        }
    }
}