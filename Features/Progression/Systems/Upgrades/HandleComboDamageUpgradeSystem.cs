using Features.Combo.Components;
using Features.Progression.Components.Upgrades;
using Leopotam.Ecs;

namespace Features.Progression.Systems.Upgrades
{
    public class HandleComboDamageUpgradeSystem : HandlePlayerUpgradeSystem<ComboDamageUpgrade, ComboData>
    {
        protected override void ApplyUpgrade(in ComboDamageUpgrade upgrade, EcsEntity entity,
            ref ComboData affectedComponent)
        {
            affectedComponent.ExtraMultiplier += upgrade.ExtraMultiplier;
        }
    }
}