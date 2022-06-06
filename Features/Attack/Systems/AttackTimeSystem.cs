using Features.Attack.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Attack.Systems
{
    public class AttackTimeSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackData, AttackDurationData> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var attackData = ref _filter.Get1(i);
                if (!attackData.Attacking) continue;

                attackData.RemainingTime -= Time.deltaTime;

                if (attackData.RemainingTime <= 0f)
                {
                    attackData.Attacking = false;
                    attackData.DealingDamage = false;
                    _filter.GetEntity(i).Replace(new AttackFinishEvent());
                }
                else
                {
                    ref var attackDurationData = ref _filter.Get2(i);
                    var attackProgress = Mathf.Clamp01(1f - attackData.RemainingTime / attackDurationData.Duration);

                    var isDealingDamageCurve =
                        attackData.AttackTypes[attackData.CurrentAttackType].IsDealingDamageOverProgress;
                    const float threshold = 0.5f;
                    attackData.DealingDamage = isDealingDamageCurve.Evaluate(attackProgress) > threshold;
                }
            }
        }
    }
}