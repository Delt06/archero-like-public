using System;
using UnityEngine;

namespace Features.Progression.Components.Upgrades
{
    [Serializable]
    public struct MaxHealthUpgrade
    {
        [Min(1f)]
        public float Multiplier;
    }
}