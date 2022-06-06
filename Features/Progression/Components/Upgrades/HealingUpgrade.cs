using System;
using UnityEngine;

namespace Features.Progression.Components.Upgrades
{
    [Serializable]
    public struct HealingUpgrade
    {
        [Range(0f, 1f)]
        public float HealProportion;
    }
}