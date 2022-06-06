using System;
using UnityEngine;

namespace Features.Progression.Components.Upgrades
{
    [Serializable]
    public struct DamageUpgrade
    {
        [Min(1f)]
        public float Multiplier;
    }
}