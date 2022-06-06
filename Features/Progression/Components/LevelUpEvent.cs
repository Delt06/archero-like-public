using Leopotam.Ecs;

namespace Features.Progression.Components
{
    public struct LevelUpEvent : IEcsAutoReset<LevelUpEvent>
    {
        public EcsEntity Entity;

        public void AutoReset(ref LevelUpEvent c)
        {
            c.Entity = EcsEntity.Null;
        }
    }
}