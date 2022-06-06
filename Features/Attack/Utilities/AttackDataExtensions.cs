using System.Runtime.CompilerServices;
using Features.Attack.Assets;
using Features.Attack.Components;

namespace Features.Attack.Utilities
{
    public static class AttackDataExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static AttackTypeAsset GetCurrentAttackType(this in AttackData attackData) =>
            attackData.AttackTypes[attackData.CurrentAttackType];
    }
}