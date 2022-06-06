using System;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Components
{
    [Serializable]
    public struct MovementData : IEcsAutoReset<MovementData>
    {
        public bool IsMoving;
        public Vector3 Direction;
        public float Speed;

        public void AutoReset(ref MovementData c)
        {
            c.IsMoving = false;
            c.Direction = Vector3.zero;
        }
    }
}