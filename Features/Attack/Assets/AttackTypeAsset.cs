using Features.Attack.Behaviours;
using UnityEngine;

namespace Features.Attack.Assets
{
    [CreateAssetMenu]
    public class AttackTypeAsset : ScriptableObject
    {
        [Min(0)]
        public int IndexInAnimator = 0;
        public AnimationCurve IsDealingDamageOverProgress = AnimationCurve.Constant(0f, 1f, 0f);
        public Hand Hand = Hand.Right;
    }
}