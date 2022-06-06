using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Components
{
    [Serializable]
    public struct RotationData : IEcsAutoReset<RotationData>
    {
        public float RotationSpeed;
        public bool HasTargetRotation;
        public Quaternion TargetRotation;

        public void AutoReset(ref RotationData c)
        {
            c.HasTargetRotation = false;
            c.TargetRotation = Quaternion.identity;
        }
    }
}