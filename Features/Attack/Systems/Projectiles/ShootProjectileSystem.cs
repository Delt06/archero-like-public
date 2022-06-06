using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Attack.Behaviours.Pooling;
using Features.Attack.Components;
using Features.Attack.Components.Projectiles;
using Features.Characters.Components;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Attack.Systems.Projectiles
{
    public class ShootProjectileSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackData, RangedData, ProjectileCreationData, TeamData, DamageData> _filter =
            default;
        private readonly ArrowViewPool _arrowPool;

        public ShootProjectileSystem(ArrowViewPool arrowPool) => _arrowPool = arrowPool;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var attackData = ref _filter.Get1(i);
                if (!attackData.DealingDamage) continue;

                ref var rangedData = ref _filter.Get2(i);
                if (rangedData.Shot) continue;

                ref var projectileCreationData = ref _filter.Get3(i);
                var spawnPoint = projectileCreationData.SpawnPoint;
                var position = spawnPoint.position;
                var rotation = ToYRotation(spawnPoint.rotation);
                var entity = _filter.GetEntity(i);

                if (TryGetRotationTowardsTarget(entity, position, out var rotationTowardsTarget))
                {
                    var exactRotationThreshold = projectileCreationData.ExactRotationThreshold;
                    SnapToTargetIfCloseEnough(ref rotation, rotationTowardsTarget, exactRotationThreshold);
                }

                var projectileView = _arrowPool.Create(position, rotation);
                ref var projectileData = ref projectileView.Entity.Get<ProjectileData>();
                projectileData.Team = _filter.Get4(i).Team;
                projectileData.Damage = _filter.Get5(i).Damage;
                projectileData.RemainingTime = rangedData.ProjectileLifetime;

                ref var projectileCreationEvent = ref entity.Get<ProjectileCreationEvent>();
                projectileCreationEvent.Projectile = projectileView.Entity;

                rangedData.Shot = true;
            }
        }

        private static Quaternion ToYRotation(in Quaternion rotation)
        {
            var eulerAngles = rotation.eulerAngles;
            eulerAngles.x = eulerAngles.z = 0f;
            return Quaternion.Euler(eulerAngles);
        }

        private static bool TryGetRotationTowardsTarget(in EcsEntity entity, in Vector3 position,
            out Quaternion rotationTowardsTarget)
        {
            if (entity.Has<AttackTarget>())
            {
                ref var attackTarget = ref entity.Get<AttackTarget>();
                var target = attackTarget.Target;
                if (!target.TryGet(out Position targetPosition))
                {
                    rotationTowardsTarget = default;
                    return false;
                }

                var direction = (targetPosition.WorldPosition - position).normalized;
                rotationTowardsTarget = ToYRotation(Quaternion.LookRotation(direction, Vector3.up));
                return true;
            }

            rotationTowardsTarget = default;
            return false;
        }

        private static void SnapToTargetIfCloseEnough(ref Quaternion rotation, in Quaternion rotationTowardsTarget,
            float threshold)
        {
            var angleDifference = Quaternion.Angle(rotation, rotationTowardsTarget);
            if (angleDifference <= threshold)
                rotation = rotationTowardsTarget;
        }
    }
}