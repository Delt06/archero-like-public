using System;
using UnityEngine;

namespace Features.Progression.Components.Upgrades
{
    [Serializable]
    public struct CriticalStrikeProbabilityUpgrade
    {
        [Range(0f, 1f)]
        public float ExtraProbability;
    }
}