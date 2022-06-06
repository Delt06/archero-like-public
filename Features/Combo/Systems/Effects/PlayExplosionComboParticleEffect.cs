using Features.Combo.Components.Effects;
using Leopotam.Ecs;

namespace Features.Combo.Systems.Effects
{
    public class PlayExplosionComboParticleEffect : IEcsRunSystem
    {
        private readonly EcsFilter<ComboExplosionEffectParticleEffectData, ComboEffectCommand<ComboExplosionEffectData>>
            _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var particleEffectData = ref _filter.Get1(i);
                particleEffectData.ParticleSystem.Play();
            }
        }
    }
}