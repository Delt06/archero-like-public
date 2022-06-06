using System;
using UnityEngine;

namespace Features.Attack.Components
{
    [Serializable]
    public struct AttackDurationData
    {
        [Min(0f)]
        public float Duration;
    }
}