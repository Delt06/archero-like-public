using System;
using UnityEngine;

namespace Features.Progression.Components.Upgrades
{
    [Serializable]
    public struct ComboBurningUpgrade
    {
        [Min(0f)]
        public float DamagePeriod;

        [Min(0f)]
        public float Duration;

        public AnimationCurve PeriodOverLevel;
        public AnimationCurve DamageMultiplierOverLevel;
    }
}