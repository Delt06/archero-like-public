using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Health.Components
{
    [Serializable]
    public struct HealthData : IEcsAutoReset<HealthData>
    {
        [Min(0f)]
        public float MaxHealth;
        [Min(0f)]
        public float Health;

        public bool IsAlive;

        public void AutoReset(ref HealthData c)
        {
            c.Health = c.MaxHealth;
            c.IsAlive = true;
        }
    }
}