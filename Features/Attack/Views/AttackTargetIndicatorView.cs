using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Views;
using DG.Tweening;
using DOTweenExtensions;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Attack.Views
{
    public sealed class AttackTargetIndicatorView : EntityView
    {
        [SerializeField] [Required] private Renderer _indicator = default;
        [SerializeField] [Required] [Min(0f)] private float _floatAmplitude = 0.25f;
        [SerializeField] [Min(0f)] private float _period = 1f;
        [SerializeField] private Ease _ease = Ease.InOutSine;

        private EcsEntity _target;
        private bool _hasTarget;
        private Vector3 _indicatorInitialLocalPosition;
        private readonly ControlledSequence _sequence = new ControlledSequence();

        public void ShowAbove(EcsEntity target)
        {
            ToggleIndicatorVisibility(true);
            _hasTarget = true;
            _target = target;
        }

        public void Hide()
        {
            ToggleIndicatorVisibility(false);
            _hasTarget = false;
            _target = default;
        }

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity.ReplaceViewBackRef(this);
        }

        private void ToggleIndicatorVisibility(bool visible)
        {
            if (_indicator.enabled != visible)
                _indicator.enabled = visible;
        }

        private void LateUpdate()
        {
            if (!_hasTarget) return;

            if (!_target.IsAlive() || !_target.Has<Position>())
                _hasTarget = false;
            else
                transform.position = _target.Get<Position>().WorldPosition;
        }

        private void OnEnable()
        {
            var highPosition = _indicatorInitialLocalPosition + Vector3.up * _floatAmplitude;
            var lowPosition = _indicatorInitialLocalPosition + Vector3.down * _floatAmplitude;
            var halfPeriod = _period * 0.5f;
            _indicator.transform.localPosition = lowPosition;
            _sequence.RecreateSequence()
                .Append(_indicator.transform.DOMoveY(highPosition.y, halfPeriod).SetEase(_ease))
                .Append(_indicator.transform.DOMoveY(lowPosition.y, halfPeriod).SetEase(_ease))
                .SetLoops(-1);
        }

        private void OnDisable()
        {
            _sequence.Kill();
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            _indicatorInitialLocalPosition = _indicator.transform.localPosition;
        }
    }
}