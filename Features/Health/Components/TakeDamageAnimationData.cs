using System;
using DG.Tweening;

namespace Features.Health.Components
{
    [Serializable]
    public struct TakeDamageAnimationData
    {
        public float IncreaseDuration;
        public Ease IncreaseEase;
        public float DecreaseDuration;
        public Ease DecreaseEase;
        public int LayerIndex;
    }
}