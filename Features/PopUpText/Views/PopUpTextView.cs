using DELTation.LeoEcsExtensions.Views;
using DG.Tweening;
using DOTweenExtensions;
using Features.PopUpText.Components;
using Leopotam.Ecs;
using TMPro;
using UnityEngine;

namespace Features.PopUpText.Views
{
    public class PopUpTextView : EntityView
    {
        [SerializeField] private TMP_Text _text = default;
        [SerializeField] private Vector3 _rootOffset = Vector3.up;
        [SerializeField] private Vector3 _motion = Vector3.up;
        [SerializeField] private Ease _motionEase = Ease.OutBack;
        [SerializeField] [Min(0f)] private float _growDuration = 0.1f;
        [SerializeField] private Ease _growEase = Ease.OutBack;
        [SerializeField] [Min(0f)] private float _shrinkDuration = 0.5f;
        [SerializeField] private Ease _shrinkEase = Ease.InExpo;

        public void Show(string format, float value, Color color, float? duration = null, Vector3? motion = null)
        {
            _text.color = color;
            _text.SetText(format, value);

            transform.localScale = Vector3.zero;
            transform.position += _rootOffset;
            var targetPosition = transform.position + (motion ?? _motion);

            _controlledSequence.RecreateSequence()
                .Append(transform.DOMove(targetPosition, _growDuration + _shrinkDuration).SetEase(_motionEase))
                .Join(DOTween.Sequence()
                    .Append(transform.DOScale(1f, _growDuration).SetEase(_growEase))
                    .Append(transform.DOScale(0f, duration ?? _shrinkDuration).SetEase(_shrinkEase))
                );
        }

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity.ReplaceViewBackRef(this);
        }

        private void OnEnable()
        {
            _text.color = Color.white;
        }

        private void OnDisable()
        {
            _controlledSequence?.Kill();
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            _controlledSequence = new ControlledSequence(() => Entity.Get<PopUpAnimationEndedTag>());
        }

        private ControlledSequence _controlledSequence;
    }
}