using DELTation.LeoEcsExtensions.Utilities;
using Features.Attack.Components;
using Features.Combo.Components.Effects;
using Leopotam.Ecs;

namespace Features.Attack.Systems
{
    public class PlayBurningParticleEffect : IEcsRunSystem
    {
        private readonly EcsFilter<BurningData> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var burningData = ref _filter.Get1(i);
                if (!burningData.Target.TryGet(out BurningParticleEffectData particleEffectData)) continue;

                var particleSystem = particleEffectData.ParticleSystem;
                if (!particleSystem.isEmitting)
                    particleSystem.Play();
            }
        }
    }
}