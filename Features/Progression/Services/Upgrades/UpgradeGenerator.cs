using System.Collections.Generic;
using System.Linq;
using Features.Progression.Assets.Upgrades;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Progression.Services.Upgrades
{
    public class UpgradeGenerator : MonoBehaviour, IUpgradeGenerator
    {
        [SerializeField] [Required] [AssetList]
        private UpgradeAsset[] _upgrades = default;

        [SerializeField] [Range(1, 3)] private int _proposedUpgradesNumber = 3;

        public void AddSystems(EcsSystems systems)
        {
            foreach (var upgrade in _upgrades)
            {
                upgrade.AddSystems(systems);
            }
        }

        public void PopulateWithDistinctUpgrades(List<UpgradeAsset> upgrades)
        {
            var proposedUpgrades = _upgrades
                .OrderBy(_ => Random.value)
                .Take(_proposedUpgradesNumber);
            upgrades.AddRange(proposedUpgrades);
        }
    }
}