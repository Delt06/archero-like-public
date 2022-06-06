using System;
using UnityEngine;

namespace Features.Attack.Components
{
    [Serializable]
    public struct CriticalStrikeData
    {
        [Min(1f)]
        public float DamageMultiplier;

        [Range(0f, 1f)]
        public float Probability;
    }
}