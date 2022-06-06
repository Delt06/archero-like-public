using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Attack.Components
{
    [Serializable]
    public struct BurningParticleEffectData
    {
        [Required]
        public ParticleSystem ParticleSystem;
    }
}