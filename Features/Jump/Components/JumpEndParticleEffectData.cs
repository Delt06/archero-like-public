using System;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Jump.Components
{
    [Serializable]
    public struct JumpEndParticleEffectData : IEcsAutoReset<JumpEndParticleEffectData>
    {
        [Required]
        public ParticleSystem ParticleSystem;

        public void AutoReset(ref JumpEndParticleEffectData c)
        {
            c.ParticleSystem = null;
        }
    }
}