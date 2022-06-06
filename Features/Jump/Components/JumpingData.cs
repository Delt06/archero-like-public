using System;
using DG.Tweening;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Jump.Components
{
    [Serializable]
    public struct JumpingData : IEcsAutoReset<JumpingData>
    {
        [Min(0f)]
        public float Cooldown;
        [Min(0f)]
        public float Height;
        [Min(0f)]
        public float ExtraHeight;
        [Min(0f)]
        public float UpDuration;
        [Min(0f)]
        public float DownDuration;
        public Ease UpEase;
        public Ease DownEase;

        public float TimeToNextJump;

        [Min(0f)]
        public float DistanceFromTarget;

        public void AutoReset(ref JumpingData c)
        {
            c.TimeToNextJump = 0f;
        }
    }
}