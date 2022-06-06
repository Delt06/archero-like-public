using System;
using UnityEngine;

namespace Features.Jump.Components
{
    [Serializable]
    public struct JumpExplosionData
    {
        [Min(0f)]
        public float PushRadius;

        [Min(0f)]
        public float Damage;
    }
}