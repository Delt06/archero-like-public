using System;
using UnityEngine;

namespace Features.Progression.Components.Upgrades
{
    [Serializable]
    public struct ComboExplosionUpgrade
    {
        [Min(0f)]
        public float Radius;
        public AnimationCurve PeriodOverLevel;
        public AnimationCurve DamageMultiplierOverLevel;
    }
}