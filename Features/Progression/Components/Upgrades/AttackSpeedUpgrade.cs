using System;
using UnityEngine;

namespace Features.Progression.Components.Upgrades
{
    [Serializable]
    public struct AttackSpeedUpgrade
    {
        [Min(1f)]
        public float Multiplier;
    }
}