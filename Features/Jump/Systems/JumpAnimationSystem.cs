using DELTation.LeoEcsExtensions.Components;
using Features.Jump.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Jump.Systems
{
    public class JumpAnimationSystem : IEcsRunSystem
    {
        private static readonly int JumpedId = Animator.StringToHash("Jumped");
        private readonly EcsFilter<UnityObjectData<Animator>, JumpEvent> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                Animator animator = _filter.Get1(i);
                animator.SetTrigger(JumpedId);
            }
        }
    }
}