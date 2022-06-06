using System;
using System.Text;
using DELTation.LeoEcsExtensions.Views;
using DG.Tweening;
using DOTweenExtensions;
using Features.Progression.Services.Upgrades;
using JetBrains.Annotations;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Features.Progression.Views.Upgrades
{
    public class AppliedUpgradeInfoView : EntityView
    {
        [SerializeField] [Required] private TMP_Text _name = default;
        [SerializeField] [Required] private TMP_Text _description = default;
        [SerializeField] [Min(0f)] private float _appearDuration = 0.5f;
        [SerializeField] private Ease _appearEase = Ease.OutQuad;
        [SerializeField] [Min(0f)] private float _stayDuration;
        [SerializeField] [Min(0f)] private float _disappearDuration = 0.5f;
        [SerializeField] private Ease _disappearEase = Ease.InQuad;

        public void Show([NotNull] IUpgradeInfo upgradeInfo)
        {
            if (upgradeInfo == null) throw new ArgumentNullException(nameof(upgradeInfo));
            _name.SetText(_stringBuilder.Clear().Append(upgradeInfo.Name));
            _description.SetText(_stringBuilder.Clear().Append(upgradeInfo.Description));

            transform.localScale = Vector3.zero;
            _controlledSequence.RecreateSequence()
                .Append(transform.DOScale(1f, _appearDuration).SetEase(_appearEase))
                .AppendInterval(_stayDuration)
                .Append(transform.DOScale(0f, _disappearDuration).SetEase(_disappearEase));
        }

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity.ReplaceViewBackRef(this);
        }

        private void OnEnable()
        {
            transform.localScale = Vector3.zero;
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            _controlledSequence?.Kill();
        }

        private readonly ControlledSequence _controlledSequence = new ControlledSequence();
        private readonly StringBuilder _stringBuilder = new StringBuilder();
    }
}