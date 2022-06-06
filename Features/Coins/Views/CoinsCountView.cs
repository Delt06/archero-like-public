using DELTation.LeoEcsExtensions.Views;
using DG.Tweening;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Features.Coins.Views
{
    public class CoinsCountView : EntityView
    {
        [SerializeField] [Required] private TMP_Text _text = default;
        [SerializeField] [Required] private string _format = "{0:0}";
        [SerializeField] [Required] private TMP_Text _increaseText = default;
        [SerializeField] [Min(0f)] private float _scaleUpDuration = 0.25f;
        [SerializeField] private Ease _scaleUpEase = Ease.OutBack;
        [SerializeField] [Min(0f)] private float _steadyDuration = 0.25f;
        [SerializeField] [Min(0f)] private float _scaleDownDuration = 0.25f;
        [SerializeField] private Ease _scaleDownEase = Ease.InOutQuad;

        public void Show(int count)
        {
            _text.SetText(_format, count);
        }

        public void ShowChange(int amount)
        {
            _increaseText.DOKill();
            var format = amount > 0 ? "+{0:0}" : "{0:0}";
            _increaseText.SetText(format, amount);
            _increaseText.transform.localScale = Vector3.zero;
            DOTween.Sequence()
                .Append(_increaseText.transform.DOScale(1f, _scaleUpDuration).SetEase(_scaleUpEase))
                .AppendInterval(_steadyDuration)
                .Append(_increaseText.transform.DOScale(0f, _scaleDownDuration).SetEase(_scaleDownEase))
                .SetId(_increaseText)
                .SetRecyclable(true);
        }

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity.ReplaceViewBackRef(this);
        }

        private void OnEnable()
        {
            _increaseText.transform.localScale = Vector3.zero;
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            _increaseText.DOKill();
        }
    }
}