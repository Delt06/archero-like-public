using Features.Characters;
using UnityEngine;

namespace Features.Movement.Components
{
    public struct ExplosionData
    {
        public Vector3 Center;
        public float PushRadius;
        public Team? Team;
        public float? Damage;
    }
}