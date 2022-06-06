using Features.Health.Components;
using Leopotam.Ecs;

namespace Features.Health.Systems
{
    public class HitParticleEffectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<TakeDamageEvent> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var takeDamageEvent = ref _filter.Get1(i);
                var target = takeDamageEvent.Target;
                if (!target.Has<HitParticleEffectData>()) continue;

                var particleSystem = target.Get<HitParticleEffectData>().ParticleSystem;
                particleSystem.Play();
            }
        }
    }
}