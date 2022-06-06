using DELTation.LeoEcsExtensions.Views;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

namespace Features.Progression.Views.Stages
{
    public class StageNumberView : EntityView
    {
        [SerializeField] [Required] private TextMeshPro _text = default;

        public void Show(int stageIndex)
        {
            var stageNumber = stageIndex + 1;
            _text.SetText("{0:0}", stageNumber);
        }

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity.ReplaceViewBackRef(this);
        }
    }
}