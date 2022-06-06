using System;
using Features.Characters;
using UnityEngine;

namespace Features.Attack.Components.Projectiles
{
    [Serializable]
    public struct ProjectileData
    {
        public Team Team;
        [Min(0f)]
        public float Damage;
        [Min(0f)]
        public float Speed;
        public float RemainingTime;
    }
}