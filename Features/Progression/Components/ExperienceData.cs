using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Progression.Components
{
    [Serializable]
    public struct ExperienceData : IEcsAutoReset<ExperienceData>
    {
        [Min(1)]
        public int Level;
        [Min(0f)]
        public float Experience;

        public void AutoReset(ref ExperienceData c)
        {
            c.Level = 1;
            c.Experience = 0f;
        }
    }
}