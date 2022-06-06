using DELTation.LeoEcsExtensions.Components;
using Features.Health.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Health.Systems
{
    public class DeathAnimationSystem : IEcsRunSystem
    {
        private static readonly int IsAliveId = Animator.StringToHash("IsAlive");
        private readonly EcsFilter<UnityObjectData<Animator>, HealthData> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                Animator animator = _filter.Get1(i);
                var healthData = _filter.Get2(i);
                animator.SetBool(IsAliveId, healthData.IsAlive);
            }
        }
    }
}