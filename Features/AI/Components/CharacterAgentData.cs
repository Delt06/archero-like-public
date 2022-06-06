using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.AI.Components
{
    [Serializable]
    public struct CharacterAgentData : IEcsAutoReset<CharacterAgentData>
    {
        [Min(0f)]
        public float StoppingDistance;
        public float RepathMinInterval;
        public float RepathMaxInterval;
        public float TimeBeforeRepath;
        [Min(0f)]
        public float DesiredDistance;

        public void AutoReset(ref CharacterAgentData c)
        {
            c.TimeBeforeRepath = 0f;
        }
    }
}