using Features.Combo.Components.Effects;

namespace Features.Progression.Components.Upgrades
{
    public struct ComboEffectLevelData<TComboEffect> where TComboEffect : struct, IComboEffectData
    {
        public int Level;
    }
}