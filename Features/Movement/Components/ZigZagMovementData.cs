using System;
using UnityEngine;

namespace Features.Movement.Components
{
    [Serializable]
    public struct ZigZagMovementData
    {
        [Min(0f)]
        public float BounceCooldown;

        public float BounceCooldownRemainingTime;

        [Min(0f)]
        public float MinMagnitudeXZ;
    }
}