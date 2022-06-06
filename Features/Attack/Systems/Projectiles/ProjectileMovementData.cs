using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Attack.Components.Projectiles;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Attack.Systems.Projectiles
{
    public class ProjectileMovementSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Position, Rotation, ProjectileData> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var position = ref _filter.Get1(i);
                var forward = _filter.Get2(i).WorldRotation * Vector3.forward;
                var speed = _filter.Get3(i).Speed;
                var newPosition = position.WorldPosition + forward * speed * Time.deltaTime;
                _filter.GetEntity(i).UpdatePosition(ref position, newPosition);
            }
        }
    }
}