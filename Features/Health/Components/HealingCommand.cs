using Leopotam.Ecs;

namespace Features.Health.Components
{
    public struct HealingCommand
    {
        public EcsEntity Target;
        public float RatioOfMaxHealth;
    }
}