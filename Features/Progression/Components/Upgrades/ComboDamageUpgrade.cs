using System;
using UnityEngine;

namespace Features.Progression.Components.Upgrades
{
    [Serializable]
    public struct ComboDamageUpgrade
    {
        [Min(0f)]
        public float ExtraMultiplier;
    }
}