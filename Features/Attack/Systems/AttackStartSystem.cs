using Features.Attack.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Attack.Systems
{
    public class AttackStartSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackData, AttackDurationData, AttackStartEvent> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var attackData = ref _filter.Get1(i);
                if (attackData.Attacking)
                {
                    _filter.GetEntity(i).Del<AttackStartEvent>();
                }
                else
                {
                    ref var attackDurationData = ref _filter.Get2(i);
                    var duration = attackDurationData.Duration;
                    attackData.Attacking = true;
                    attackData.RemainingTime = duration;
                    attackData.CurrentAttackType = Random.Range(0, attackData.AttackTypes.Length);
                }
            }
        }
    }
}