using Features.Combo.Components.Effects;
using Features.Progression.Components.Upgrades;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Progression.Systems.Upgrades
{
    public class HandleComboBurningUpgradeSystem : HandlePlayerUpgradeSystem<ComboBurningUpgrade>
    {
        protected override void ApplyUpgrade(in ComboBurningUpgrade upgrade, EcsEntity entity)
        {
            ref var levelData = ref entity.Get<ComboEffectLevelData<ComboBurningEffectData>>();
            levelData.Level++;
            var currentLevel = levelData.Level;
            ref var effectData = ref entity.Get<ComboBurningEffectData>();
            effectData.Duration = upgrade.Duration;
            effectData.DamagePeriod = upgrade.DamagePeriod;
            effectData.Period = Mathf.RoundToInt(upgrade.PeriodOverLevel.Evaluate(currentLevel));
            effectData.DamageMultiplier = upgrade.DamageMultiplierOverLevel.Evaluate(currentLevel);
        }
    }
}