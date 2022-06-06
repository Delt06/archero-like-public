using DELTation.LeoEcsExtensions.Views;
using DELTation.UI.Screens;
using Features.Progression.Components;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.UI.Views
{
    public class LoseScreenView : EntityView
    {
        [SerializeField] [Required] private GameScreen _screen = default;
        [SerializeField] [Required] private GameScreen _gameplayScreen = default;

        public void Open()
        {
            _screen.Open();
            _gameplayScreen.Close();
        }

        public void Restart()
        {
            var resetProgressEntity = World.NewEntity();
            resetProgressEntity.Get<ResetSessionDataCommand>();
            var sceneLoadEntity = World.NewEntity();
            sceneLoadEntity.Get<StageSceneLoadCommand>();
        }

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity.ReplaceViewBackRef(this);
        }
    }
}