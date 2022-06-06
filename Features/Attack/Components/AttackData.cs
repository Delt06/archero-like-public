using System;
using Features.Attack.Assets;
using Leopotam.Ecs;
using Sirenix.OdinInspector;

namespace Features.Attack.Components
{
    [Serializable]
    public struct AttackData : IEcsAutoReset<AttackData>
    {
        public bool Attacking;
        public float RemainingTime;
        public bool DealingDamage;

        [Required] [AssetSelector]
        public AttackTypeAsset[] AttackTypes;
        public int CurrentAttackType;

        public void AutoReset(ref AttackData c)
        {
            c.Attacking = false;
            c.RemainingTime = 0f;
            c.DealingDamage = false;
            c.CurrentAttackType = 0;
        }
    }
}