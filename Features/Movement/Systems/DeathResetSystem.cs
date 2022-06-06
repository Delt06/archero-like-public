using Features.Health.Components;
using Leopotam.Ecs;

namespace Features.Movement.Systems
{
    public sealed class DeathResetSystem<T> : IEcsRunSystem where T : struct, IEcsAutoReset<T>
    {
        private readonly EcsFilter<T, DeathTag> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var component = _filter.Get1(i);
                component.AutoReset(ref component);
            }
        }
    }
}