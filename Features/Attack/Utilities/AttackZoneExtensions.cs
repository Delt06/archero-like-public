using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using Features.Attack.Behaviours;
using Features.Attack.Components;
using Leopotam.Ecs;

namespace Features.Attack.Utilities
{
    public static class AttackZoneDataExtensions
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static HashSet<EcsEntity> GetEntitiesInside(this in AttackZoneData attackData, Hand hand) =>
            hand switch
            {
                Hand.Right => attackData.EntitiesInsideRight,
                Hand.Left => attackData.EntitiesInsideLeft,
                _ => throw new ArgumentOutOfRangeException(nameof(hand), hand, null),
            };
    }
}