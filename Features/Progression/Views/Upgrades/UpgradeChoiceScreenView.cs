using System;
using System.Collections.Generic;
using DELTation.LeoEcsExtensions.Views;
using DELTation.UI.Screens;
using DG.Tweening;
using Features.Progression.Assets.Upgrades;
using Features.Progression.Behaviours.Upgrades;
using Features.Progression.Components.Upgrades;
using Features.TimeUpdate.Services;
using JetBrains.Annotations;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Progression.Views.Upgrades
{
    public class UpgradeChoiceScreenView : EntityView
    {
        [SerializeField] [Required] private GameScreen _screen = default;
        [SerializeField] [Required] private UpgradeButton[] _upgradeButtons = default;
        [SerializeField] [Min(0f)] private float _activationDelay = 0.5f;

        private IPause _pause;
        private readonly List<UpgradeAsset> _savedUpgrades = new List<UpgradeAsset>();

        public void Construct(IPause pause)
        {
            _pause = pause;
        }

        public void Show([NotNull] IReadOnlyList<UpgradeAsset> upgrades)
        {
            if (upgrades == null) throw new ArgumentNullException(nameof(upgrades));

            _savedUpgrades.AddRange(upgrades);
            DOTween.Sequence()
                .AppendInterval(_activationDelay)
                .AppendCallback(ShowSavedUpgrades);
        }

        private void ShowSavedUpgrades()
        {
            var index = 0;
            var shownUpgradesNumber = Mathf.Min(_upgradeButtons.Length, _savedUpgrades.Count);

            for (; index < shownUpgradesNumber; index++)
            {
                var button = _upgradeButtons[index];
                button.ShowFor(_savedUpgrades[index], OnSelectedUpgrade);
            }

            for (; index < _upgradeButtons.Length; index++)
            {
                var button = _upgradeButtons[index];
                button.Hide();
            }

            _pause.RequestPause();
            _screen.Open();
            _savedUpgrades.Clear();
        }

        private void OnSelectedUpgrade([NotNull] UpgradeAsset upgrade)
        {
            if (upgrade == null) throw new ArgumentNullException(nameof(upgrade));
            var entity = World.NewEntity();
            upgrade.AddUpgradeComponentTo(entity);
            ref var upgradeInfoData = ref entity.Get<UpgradeInfoData>();
            upgradeInfoData.UpgradeInfo = upgrade;
            _pause.RequestResume();
            _screen.Close();
        }

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity.ReplaceViewBackRef(this);
        }
    }
}