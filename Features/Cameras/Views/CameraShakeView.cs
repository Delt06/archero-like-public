using Cinemachine;
using DELTation.LeoEcsExtensions.Views;
using DG.Tweening;
using DG.Tweening.Core;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Cameras.Views
{
    public class CameraShakeView : EntityView
    {
        [SerializeField] [Required] private CinemachineVirtualCamera _virtualCamera = default;
        [SerializeField] [Min(0f)] private float _duration = 0.5f;
        [SerializeField] [Min(0f)] private float _amplitudeGain = 0.5f;
        [SerializeField] private Ease _fadeEase = Ease.OutQuad;

        private CinemachineBasicMultiChannelPerlin _noise;
        private DOGetter<float> _gainGetter;
        private DOSetter<float> _gainSetter;

        public void Shake()
        {
            _noise.DOKill();
            _noise.m_AmplitudeGain = _amplitudeGain;
            DOTween.To(_gainGetter, _gainSetter, 0f, _duration)
                .SetEase(_fadeEase)
                .SetRecyclable(true)
                .SetId(_noise);
        }

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity.ReplaceViewBackRef(this);
        }

        protected override void OnAwake()
        {
            base.OnAwake();
            _noise = _virtualCamera.GetCinemachineComponent<CinemachineBasicMultiChannelPerlin>();
            _gainGetter = () => _noise.m_AmplitudeGain;
            _gainSetter = gain => _noise.m_AmplitudeGain = gain;
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            _noise.DOKill();
        }
    }
}