using System.Collections.Generic;
using Leopotam.Ecs;

namespace Features.Attack.Components
{
    public struct AttackZoneData : IEcsAutoReset<AttackZoneData>
    {
        public HashSet<EcsEntity> EntitiesInsideRight;
        public HashSet<EcsEntity> EntitiesInsideLeft;
        public HashSet<EcsEntity> AlreadyDealtDamage;

        public static AttackZoneData Create() => new AttackZoneData(new HashSet<EcsEntity>(),
            new HashSet<EcsEntity>(new HashSet<EcsEntity>()), new HashSet<EcsEntity>()
        );

        private AttackZoneData(HashSet<EcsEntity> entitiesInsideRight, HashSet<EcsEntity> entitiesInsideLeft,
            HashSet<EcsEntity> alreadyDealtDamage)
        {
            EntitiesInsideRight = entitiesInsideRight;
            EntitiesInsideLeft = entitiesInsideLeft;
            AlreadyDealtDamage = alreadyDealtDamage;
        }

        public void AutoReset(ref AttackZoneData c)
        {
            c.EntitiesInsideRight ??= new HashSet<EcsEntity>();
            c.EntitiesInsideRight.Clear();
            c.EntitiesInsideLeft ??= new HashSet<EcsEntity>();
            c.EntitiesInsideLeft.Clear();
            c.AlreadyDealtDamage ??= new HashSet<EcsEntity>();
            c.AlreadyDealtDamage.Clear();
        }
    }
}