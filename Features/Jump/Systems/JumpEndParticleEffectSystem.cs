using Features.Jump.Components;
using Leopotam.Ecs;

namespace Features.Jump.Systems
{
    public class JumpEndParticleEffectSystem : IEcsRunSystem
    {
        private readonly EcsFilter<JumpEndParticleEffectData, JumpEndEvent> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                var particleSystem = _filter.Get1(i).ParticleSystem;
                particleSystem.Play();
            }
        }
    }
}