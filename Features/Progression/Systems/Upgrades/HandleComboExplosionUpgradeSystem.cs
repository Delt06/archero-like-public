using Features.Combo.Components.Effects;
using Features.Progression.Components.Upgrades;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Progression.Systems.Upgrades
{
    public class HandleComboExplosionUpgradeSystem : HandlePlayerUpgradeSystem<ComboExplosionUpgrade>
    {
        protected override void ApplyUpgrade(in ComboExplosionUpgrade upgrade, EcsEntity entity)
        {
            ref var levelData = ref entity.Get<ComboEffectLevelData<ComboExplosionEffectData>>();
            levelData.Level++;
            var currentLevel = levelData.Level;
            ref var effectData = ref entity.Get<ComboExplosionEffectData>();
            effectData.Radius = upgrade.Radius;
            effectData.Period = Mathf.RoundToInt(upgrade.PeriodOverLevel.Evaluate(currentLevel));
            effectData.DamageMultiplier = upgrade.DamageMultiplierOverLevel.Evaluate(currentLevel);
        }
    }
}