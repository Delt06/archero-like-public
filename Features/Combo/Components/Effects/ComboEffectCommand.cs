using Leopotam.Ecs;

namespace Features.Combo.Components.Effects
{
    public struct ComboEffectCommand<T> : IEcsIgnoreInFilter where T : struct, IComboEffectData { }
}