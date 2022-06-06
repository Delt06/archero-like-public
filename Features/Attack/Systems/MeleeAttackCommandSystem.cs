using Features.Attack.Behaviours;
using Features.Attack.Components;
using Features.Attack.Utilities;
using Features.Characters;
using Features.Characters.Components;
using Leopotam.Ecs;

namespace Features.Attack.Systems
{
    public class MeleeAttackCommandSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackData, AttackZoneData, DamageData, TeamData, MeleeData> _filter = default;
        private readonly EcsWorld _world = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var attackData = ref _filter.Get1(i);
                if (!attackData.DealingDamage) continue;

                ref var attackZoneData = ref _filter.Get2(i);
                var entity = _filter.GetEntity(i);
                var currentAttackType = attackData.GetCurrentAttackType();
                var hand = currentAttackType.Hand;
                var team = _filter.Get4(i).Team;
                ref var damageData = ref _filter.Get3(i);
                TryDealDamageToAllEntitiesInside(entity, hand, attackZoneData, team, damageData.Damage);
            }
        }

        private void TryDealDamageToAllEntitiesInside(EcsEntity thisEntity, Hand hand,
            in AttackZoneData attackZoneData,
            Team team,
            float damage)
        {
            var entitiesInside = attackZoneData.GetEntitiesInside(hand);
            foreach (var otherEntity in entitiesInside)
            {
                if (!otherEntity.IsAlive()) continue;
                if (otherEntity == thisEntity) continue;
                if (attackZoneData.AlreadyDealtDamage.Contains(otherEntity)) continue;

                ref var attackCommand = ref _world.NewEntity().Get<AttackCommand>();
                attackCommand.Creator = thisEntity;
                attackCommand.TakeDamageCommand = new TakeDamageCommand
                {
                    Damage = damage, SourceTeam = team, Target = otherEntity,
                };
                attackZoneData.AlreadyDealtDamage.Add(otherEntity);
            }
        }
    }
}