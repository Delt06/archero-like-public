using System;
using UnityEngine;

namespace Features.Loot.Components
{
    [Serializable]
    public struct CoinLootData
    {
        [Min(0)]
        public int MinCount;
        [Min(0)]
        public int MaxCount;
    }
}