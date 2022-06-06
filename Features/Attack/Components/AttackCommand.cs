using Leopotam.Ecs;

namespace Features.Attack.Components
{
    public struct AttackCommand
    {
        public EcsEntity Creator;
        public TakeDamageCommand TakeDamageCommand;
    }
}