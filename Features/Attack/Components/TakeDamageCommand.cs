using Features.Characters;
using Leopotam.Ecs;

namespace Features.Attack.Components
{
    public struct TakeDamageCommand
    {
        public EcsEntity Target;
        public float Damage;
        public Team? SourceTeam;
    }
}