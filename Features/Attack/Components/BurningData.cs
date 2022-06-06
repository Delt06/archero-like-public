using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Attack.Components
{
    [Serializable]
    public struct BurningData
    {
        public EcsEntity Target;
        [Min(0f)]
        public float RemainingTime;
        [Min(0f)]
        public float TimeTillNextDamage;
        [Min(0f)]
        public float Period;
        [Min(0f)]
        public float Damage;
    }
}