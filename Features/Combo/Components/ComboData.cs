using System;
using UnityEngine;

namespace Features.Combo.Components
{
    [Serializable]
    public struct ComboData
    {
        [Min(0f)]
        public float Cooldown;
        public AnimationCurve ExtraDamageOverHits;
        [Min(0f)]
        public float ExtraMultiplier;
    }
}