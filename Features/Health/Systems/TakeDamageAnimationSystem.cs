using DELTation.LeoEcsExtensions.Components;
using DG.Tweening;
using Features.Health.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Health.Systems
{
    public class TakeDamageAnimationSystem : IEcsRunSystem
    {
        private static readonly int TakeDamageId = Animator.StringToHash("TakeDamage");
        private readonly EcsFilter<TakeDamageEvent> _events = default;

        public void Run()
        {
            foreach (var i in _events)
            {
                ref var @event = ref _events.Get1(i);
                var target = @event.Target;
                Animator animator = target.Get<UnityObjectData<Animator>>();
                animator.SetTrigger(TakeDamageId);

                ref var animationData = ref target.Get<TakeDamageAnimationData>();
                var layerIndex = animationData.LayerIndex;

                float Getter() => animator.GetLayerWeight(layerIndex);
                void Setter(float weight) => animator.SetLayerWeight(layerIndex, weight);

                DOTween.Sequence()
                    .Append(DOTween.To(Getter, Setter, 1f, animationData.IncreaseDuration)
                        .SetEase(animationData.IncreaseEase)
                    )
                    .Append(DOTween.To(Getter, Setter, 0f, animationData.DecreaseDuration)
                        .SetEase(animationData.DecreaseEase)
                    );
            }
        }
    }
}