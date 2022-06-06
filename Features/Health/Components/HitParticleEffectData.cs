using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Health.Components
{
    [Serializable]
    public struct HitParticleEffectData
    {
        [Required]
        public ParticleSystem ParticleSystem;
    }
}