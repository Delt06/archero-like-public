using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Attack.Components;
using Features.Health.Components;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Attack.Systems
{
    public class AutoAttackSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AutoAttackData, Position, Rotation, AttackTarget>.Exclude<DeathTag> _filter =
            default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var autoAttackData = _filter.Get1(i);
                ref var position = ref _filter.Get2(i);
                ref var rotation = ref _filter.Get3(i);
                var target = _filter.Get4(i).Target;

                if (ShouldAutoAttack(autoAttackData, position, rotation, target))
                {
                    var entity = _filter.GetEntity(i);
                    entity.Replace(new AttackStartEvent());
                }
            }
        }

        private static bool ShouldAutoAttack(AutoAttackData attackData, Position position, Rotation rotation,
            EcsEntity target)
        {
            if (!target.TryGet(out Position targetPosition)) return false;
            var offset = targetPosition.WorldPosition - position.WorldPosition;
            var closeEnough = offset.sqrMagnitude <= attackData.MaxDistance * attackData.MaxDistance;

            var direction = offset.normalized;
            var forward = rotation.WorldRotation * Vector3.forward;
            var angle = Vector3.Angle(direction, forward);
            var withinAllowedAngle = angle <= attackData.MaxAngularDifferenceToLookAt;

            return closeEnough && withinAllowedAngle;
        }
    }
}