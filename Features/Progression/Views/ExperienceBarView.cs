using System.Text;
using DELTation.LeoEcsExtensions.Views;
using DG.Tweening;
using DOTweenExtensions;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Progression.Views
{
    public class ExperienceBarView : EntityView
    {
        [SerializeField] [Required] private Image _fill = default;
        [SerializeField] [Required] private TMP_Text _levelNumber = default;
        [SerializeField] private string _levelPrefix = "Lv.";
        [SerializeField] [Min(0f)] private float _fillDuration = 0.5f;
        [SerializeField] private Ease _fillEase = Ease.OutQuad;

        public int LastDisplayedLevel { get; private set; }

        public void CompleteSequence() => _controlledSequence.CompleteIfExists();

        public void AppendUpdateFillAmountSmoothly(float targetFill) => _controlledSequence.GetOrCreateSequence()
            .Append(_fill.DOFillAmount(targetFill, _fillDuration).SetEase(_fillEase));

        public void AppendUpdateFillAmount(float fillAmount) =>
            _controlledSequence.GetOrCreateSequence().AppendCallback(() => UpdateFillAmount(fillAmount));

        public void UpdateFillAmount(float fillAmount) => _fill.fillAmount = fillAmount;

        public void AppendUpdateLevelNumber(int level) =>
            _controlledSequence.GetOrCreateSequence().AppendCallback(() => UpdateLevel(level));

        public void UpdateLevel(int level)
        {
            _stringBuilder.Clear().Append(_levelPrefix).Append(level);
            _levelNumber.SetText(_stringBuilder);
            LastDisplayedLevel = level;
        }

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity.ReplaceViewBackRef(this);
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            _controlledSequence?.Kill();
            _sequence?.Kill();
            _sequence = null;
        }

        private Sequence _sequence;
        private readonly ControlledSequence _controlledSequence = new ControlledSequence();
        private readonly StringBuilder _stringBuilder = new StringBuilder();
    }
}