using Features.Attack.Components;
using Features.Coins.Components;
using Features.Combo.Components;
using Features.Combo.Components.Effects;
using Features.Health.Components;
using Features.Progression.Components;
using Features.Progression.Components.Upgrades;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Progression.Services.Session
{
    public class SessionProgress : MonoBehaviour, ISessionProgress
    {
        public void OnPassedStage(int stageIndex)
        {
            var newLastPassedStageIndex = LastPassedStageIndex == null
                ? stageIndex
                : Mathf.Max(stageIndex, LastPassedStageIndex.Value);
            LastPassedStageIndex = newLastPassedStageIndex;
        }

        public void ResetAll()
        {
            LastPassedStageIndex = null;
            _persistentEntity.Destroy();
            CreateEntity();
        }

        public void LoadInto(in EcsEntity entity)
        {
            foreach (var handler in _handlers)
            {
                handler.Load(_persistentEntity, entity);
            }
        }

        public void SaveFrom(in EcsEntity entity)
        {
            foreach (var handler in _handlers)
            {
                handler.Save(_persistentEntity, entity);
            }
        }

        public int? LastPassedStageIndex { get; private set; }

        private void Awake()
        {
            var builder = new SessionPersistenceBuilder();
            builder
                .AddHandler<HealthData>()
                .AddHandler<ExperienceData>()
                .AddHandler<DamageData>()
                .AddHandler<AttackDurationData>()
                .AddHandler<CoinsData>()
                .AddHandler<CriticalStrikeData>()
                .AddHandler<ComboData>()
                ;
            builder
                .AddHandler<ComboExplosionEffectData>()
                .AddHandler<ComboEffectLevelData<ComboExplosionEffectData>>()
                ;
            builder
                .AddHandler<ComboBurningEffectData>()
                .AddHandler<ComboEffectLevelData<ComboBurningEffectData>>()
                ;
            _handlers = builder.Build();
            _world = new EcsWorld();
            CreateEntity();
        }

        private void CreateEntity()
        {
            _persistentEntity = _world.NewEntity();
            _persistentEntity.Get<SessionProgressTag>();
        }

        private void OnDestroy()
        {
            _world?.Destroy();
            _persistentEntity = EcsEntity.Null;
        }

        private EcsEntity _persistentEntity;
        private EcsWorld _world;
        private ISessionPersistenceHandler[] _handlers;
    }
}