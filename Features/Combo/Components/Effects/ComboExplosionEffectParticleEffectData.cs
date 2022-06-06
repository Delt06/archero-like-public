using System;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Combo.Components.Effects
{
    [Serializable]
    public struct ComboExplosionEffectParticleEffectData
    {
        [Required]
        public ParticleSystem ParticleSystem;
    }
}