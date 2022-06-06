using System;
using UnityEngine;

namespace Features.Attack.Components
{
    [Serializable]
    public struct AttackAnimationData
    {
        [Min(0f)]
        public float WeightChangeSpeed;
        [Min(0f)]
        public float WeightDecreaseSpeed;
        [Min(0)]
        public int ArmsLayerIndex;
    }
}