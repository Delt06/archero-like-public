using System;
using System.Runtime.CompilerServices;
using Features.Attack.Behaviours;

namespace Features.Attack.Utilities
{
    public static class HandExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static Hand GetOther(this Hand hand) =>
            hand switch
            {
                Hand.Right => Hand.Left,
                Hand.Left => Hand.Right,
                _ => throw new ArgumentOutOfRangeException(nameof(hand), hand, null),
            };
    }
}