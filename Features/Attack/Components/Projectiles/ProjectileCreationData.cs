using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Attack.Components.Projectiles
{
    [Serializable]
    public struct ProjectileCreationData
    {
        [Required]
        public Transform SpawnPoint;

        [Min(0f)]
        public float ExactRotationThreshold;
    }
}