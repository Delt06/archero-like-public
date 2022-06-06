using DELTation.LeoEcsExtensions.Views;
using DG.Tweening;
using DG.Tweening.Core;
using Features.Health.Components;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Health.Views
{
    public class HealthBarView : EntityView
    {
        [SerializeField] [Required] private GameObject _ownerViewGameObject = default;
        [SerializeField] private GameObject _root = default;
        [SerializeField] private Image _fillImage = default;
        [SerializeField] private Image _delayedFillImage = default;
        [SerializeField] private float _delay = 0.5f;
        [SerializeField] private Ease _delayEase = Ease.OutQuad;
        [SerializeField] private TMP_Text _text = default;

        private float? _oldHealth;
        private DOGetter<float> _fillAmountGetter;
        private DOSetter<float> _fillAmountSetter;

        public void UpdateBar(bool snap)
        {
            if (!TryGetHealthData(out var healthData)) return;

            if (_root.activeSelf != healthData.IsAlive)
                _root.SetActive(healthData.IsAlive);

            if (healthData.IsAlive) TryUpdateFill(healthData, snap);

            _oldHealth = healthData.Health;
        }

        public IEntityView OwnerView { get; private set; }

        protected override void OnAwake()
        {
            base.OnAwake();
            _fillAmountGetter = () => _delayedFillImage.fillAmount;
            _fillAmountSetter = value => _delayedFillImage.fillAmount = value;
            OwnerView = _ownerViewGameObject.GetComponent<IEntityView>();
        }

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity.ReplaceViewBackRef(this);
        }

        private void OnEnable()
        {
            _oldHealth = null;
        }

        private bool TryGetHealthData(out HealthData healthData)
        {
            if (OwnerView.TryGetEntity(out var entity) && entity.Has<HealthData>())
            {
                healthData = entity.Get<HealthData>();
                return true;
            }

            healthData = default;
            return false;
        }

        private void TryUpdateFill(HealthData healthData, bool snap)
        {
            if (_oldHealth.HasValue && !Mathf.Approximately(_oldHealth.Value, healthData.Health))
            {
                if (snap)
                    _delayedFillImage.DOKill(true);
                return;
            }

            TryUpdateText(healthData);

            var healthRatio = Mathf.Clamp01(healthData.Health / healthData.MaxHealth);
            _fillImage.fillAmount = healthRatio;
            _delayedFillImage.DOKill();

            if (snap || _oldHealth == null)
                _delayedFillImage.fillAmount = healthRatio;
            else
                DOTween.To(_fillAmountGetter, _fillAmountSetter, healthRatio, _delay)
                    .SetEase(_delayEase)
                    .SetRecyclable(true)
                    .SetId(_delayedFillImage);
        }

        private void TryUpdateText(HealthData healthData)
        {
            if (!_text) return;

            _text.SetText("{0:0}", Mathf.CeilToInt(healthData.Health));
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            _delayedFillImage.DOKill();
        }
    }
}