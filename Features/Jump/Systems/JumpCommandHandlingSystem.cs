using DELTation.LeoEcsExtensions.Components;
using DG.Tweening;
using Features.Jump.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Jump.Systems
{
    public class JumpCommandHandlingSystem : IEcsRunSystem
    {
        private readonly EcsFilter<JumpingData, UnityObjectData<Transform>, JumpCommand>.Exclude<ActiveJumpData>
            _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var jumpingData = ref _filter.Get1(i);
                Transform transform = _filter.Get2(i);
                ref var jumpCommand = ref _filter.Get3(i);
                var targetPosition = jumpCommand.TargetPosition;
                targetPosition.y = transform.position.y;
                var jumpTopPosition = targetPosition + Vector3.up * jumpingData.Height;
                DOTween.Sequence()
                    .Append(transform.DOJump(jumpTopPosition, jumpingData.ExtraHeight, 1, jumpingData.UpDuration)
                        .SetEase(jumpingData.UpEase)
                    )
                    .Append(transform.DOMove(targetPosition, jumpingData.DownDuration)
                        .SetEase(jumpingData.DownEase)
                    );

                var entity = _filter.GetEntity(i);
                var totalDuration = jumpingData.UpDuration + jumpingData.DownDuration;
                entity.Get<ActiveJumpData>().RemainingTime = totalDuration;
                entity.Get<JumpEvent>();
            }
        }
    }
}