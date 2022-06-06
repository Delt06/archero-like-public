using System;
using UnityEngine;

namespace Features.Combo.Components.Effects
{
    [Serializable]
    public struct ComboBurningEffectData : IComboEffectData
    {
        [Min(1)]
        public int Period;

        [Min(0f)]
        public float DamagePeriod;

        [Min(0f)]
        public float DamageMultiplier;

        [Min(0f)]
        public float Duration;

        int IComboEffectData.Period => Period;
    }
}