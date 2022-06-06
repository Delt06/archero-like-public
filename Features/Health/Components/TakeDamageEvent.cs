using Leopotam.Ecs;

namespace Features.Health.Components
{
    public struct TakeDamageEvent
    {
        public EcsEntity Target;
        public float Damage;
    }
}