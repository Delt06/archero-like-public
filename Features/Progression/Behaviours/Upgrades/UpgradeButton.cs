using System;
using Features.Progression.Assets.Upgrades;
using JetBrains.Annotations;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

namespace Features.Progression.Behaviours.Upgrades
{
    public class UpgradeButton : MonoBehaviour
    {
        [SerializeField] [Required] private Image _button = default;
        [SerializeField] [Required] private Image _icon = default;
        [SerializeField] [Required] private TMP_Text _text = default;

        [CanBeNull]
        private UpgradeAsset _upgrade;
        [CanBeNull]
        private Action<UpgradeAsset> _onClick;

        public void Hide()
        {
            gameObject.SetActive(false);
            _upgrade = null;
            _onClick = null;
        }

        public void ShowFor([NotNull] UpgradeAsset upgrade, [NotNull] Action<UpgradeAsset> onClick)
        {
            if (upgrade == null) throw new ArgumentNullException(nameof(upgrade));

            _upgrade = upgrade;
            _onClick = onClick ?? throw new ArgumentNullException(nameof(onClick));
            _button.sprite = upgrade.IconBackground;
            _icon.sprite = upgrade.Icon;
            _text.text = upgrade.Name;
            gameObject.SetActive(true);
        }

        public void OnClick()
        {
            if (_upgrade != null)
                _onClick?.Invoke(_upgrade);
        }
    }
}