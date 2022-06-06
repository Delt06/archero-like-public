using System;
using UnityEngine;

namespace Features.Coins.Components
{
    [Serializable]
    public struct CoinsData
    {
        [Min(0)]
        public int Count;
    }
}