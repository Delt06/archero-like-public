using System;
using UnityEngine;

namespace Features.Combo.Components.Effects
{
    [Serializable]
    public struct ComboExplosionEffectData : IComboEffectData
    {
        [Min(1)]
        public int Period;
        [Min(0f)]
        public float Radius;
        [Min(0f)]
        public float DamageMultiplier;

        int IComboEffectData.Period => Period;
    }
}