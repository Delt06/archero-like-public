using System;
using UnityEngine;

namespace Features.Loot.Components
{
    [Serializable]
    public struct HealingLootData
    {
        [Range(0f, 1f)]
        public float MinAmount;
        [Range(0f, 1f)]
        public float MaxAmount;

        [Range(0f, 1f)]
        public float Probability;
    }
}