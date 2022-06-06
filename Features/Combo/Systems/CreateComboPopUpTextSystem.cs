using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Pooling;
using Features.Combo.Components;
using Features.PopUpText.Assets;
using Features.PopUpText.Views;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Combo.Systems
{
    public class CreateComboPopUpTextSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ComboHitEvent, Position> _filter = default;
        private readonly IEntityViewPool<PopUpTextView> _pool;
        private readonly PopUpTextDataAsset _textData;

        public CreateComboPopUpTextSystem(IEntityViewPool<PopUpTextView> pool, PopUpTextDataAsset textData)
        {
            _pool = pool;
            _textData = textData;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var comboHitEvent = ref _filter.Get1(i);
                ref var position = ref _filter.Get2(i);
                var worldPosition = position.WorldPosition + _textData.ComboHitOffset;
                var view = _pool.Create(worldPosition, Quaternion.identity);
                view.Show("x{0:0}", comboHitEvent.Hits, _textData.ComboHitColor, _textData.ComboHitDuration,
                    Vector3.zero
                );
            }
        }
    }
}