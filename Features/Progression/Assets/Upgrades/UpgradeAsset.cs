using System;
using AssetIcons;
using Features.Progression.Services.Upgrades;
using Features.Progression.Systems.Upgrades;
using JetBrains.Annotations;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Progression.Assets.Upgrades
{
    public abstract class UpgradeAsset : ScriptableObject, IUpgradeInfo
    {
        [SerializeField] [Required] [PreviewField]
        private Sprite _iconBackground = default;
        [SerializeField] [Required] [PreviewField]
        private Sprite _icon = default;
        [SerializeField] [Required] private string _name = "Upgrade Name";
        [SerializeField] [Required] private string _description = "Upgrade Description";

        [AssetIcon(layer: 0)]
        public Sprite IconBackground => _iconBackground;

        [AssetIcon(layer: 1)]
        public Sprite Icon => _icon;

        public string Name => _name;

        public string Description => _description;

        public abstract void AddSystems(EcsSystems systems);
        public abstract void AddUpgradeComponentTo(EcsEntity entity);

        protected const string AssetPath = "Upgrade/";
    }

    public abstract class UpgradeAsset<TUpgrade> : UpgradeAsset where TUpgrade : struct
    {
        [SerializeField] [InlineProperty] private TUpgrade _upgrade = default;

        public sealed override void AddSystems([NotNull] EcsSystems systems)
        {
            if (systems == null) throw new ArgumentNullException(nameof(systems));
            var handleSystem = CreateHandleSystem();
            systems.Add(handleSystem);
            systems.OneFrame<TUpgrade>();
        }

        protected abstract HandleUpgradeSystem<TUpgrade> CreateHandleSystem();

        public sealed override void AddUpgradeComponentTo(EcsEntity entity)
        {
            entity.Replace(_upgrade);
        }
    }
}