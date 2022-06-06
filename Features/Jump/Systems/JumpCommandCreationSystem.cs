using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Jump.Components;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Jump.Systems
{
    public class JumpCommandCreationSystem : IEcsRunSystem
    {
        private readonly EcsFilter<JumpingData, AttackTarget, Position> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var jumpingData = ref _filter.Get1(i);
                if (jumpingData.TimeToNextJump > 0f) continue;

                jumpingData.TimeToNextJump = jumpingData.Cooldown;

                var entity = _filter.GetEntity(i);
                ref var jumpCommand = ref entity.Get<JumpCommand>();
                ref var attackTarget = ref _filter.Get2(i);
                var target = attackTarget.Target;
                if (!target.TryGet(out Position targetPositionComponent)) continue;

                var targetPosition = targetPositionComponent.WorldPosition;
                var currentPosition = _filter.Get3(i).WorldPosition;
                targetPosition =
                    Vector3.MoveTowards(targetPosition, currentPosition, jumpingData.DistanceFromTarget);
                jumpCommand.TargetPosition = targetPosition;
            }
        }
    }
}