using Leopotam.Ecs;

namespace Features.Health.Components
{
    public struct HealingEvent
    {
        public EcsEntity Target;
        public float RestoredHealth;
    }
}