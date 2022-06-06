using DELTation.LeoEcsExtensions.Components;
using Features.Attack.Components;
using Features.Attack.Utilities;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Attack.Systems
{
    public class AttackAnimationsSystem : IEcsRunSystem
    {
        private static readonly int AttackSpeedId = Animator.StringToHash("AttackSpeed");
        private static readonly int AttackTypeId = Animator.StringToHash("AttackType");
        private static readonly int AttackId = Animator.StringToHash("Attack");
        private readonly EcsFilter<AttackAnimationData, AttackData, UnityObjectData<Animator>, AttackDurationData>
            _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var animationData = ref _filter.Get1(i);
                ref var attackData = ref _filter.Get2(i);
                Animator animator = _filter.Get3(i);
                var weightSpeed = attackData.Attacking
                    ? animationData.WeightChangeSpeed
                    : -animationData.WeightDecreaseSpeed;
                var deltaWeight = Time.deltaTime * weightSpeed;

                var layerIndex = animationData.ArmsLayerIndex;
                var weight = animator.GetLayerWeight(layerIndex);
                weight = Mathf.Clamp01(weight + deltaWeight);
                animator.SetLayerWeight(layerIndex, weight);

                var entity = _filter.GetEntity(i);

                if (entity.Has<AttackStartEvent>())
                {
                    var attackDuration = _filter.Get4(i).Duration;
                    animator.SetFloat(AttackSpeedId, 1f / attackDuration);
                    var currentAttackType = attackData.GetCurrentAttackType();
                    animator.SetFloat(AttackTypeId, currentAttackType.IndexInAnimator);
                    animator.SetTrigger(AttackId);
                }
            }
        }
    }
}