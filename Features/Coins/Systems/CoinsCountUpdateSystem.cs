using DELTation.LeoEcsExtensions.Components;
using Features.Coins.Components;
using Features.Coins.Views;
using Leopotam.Ecs;

namespace Features.Coins.Systems
{
    public class CoinsCountUpdateSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<CoinsData> _dataFilter = default;
        private readonly EcsFilter<CoinsData, CoinsDataChangeEvent> _dataChangeFilter = default;
        private readonly EcsFilter<ViewBackRef<CoinsCountView>> _viewFilter = default;

        public void Init()
        {
            foreach (var iData in _dataFilter)
            {
                ref var coinsData = ref _dataFilter.Get1(iData);
                var count = coinsData.Count;
                ShowForAllViews(count, null);
                break;
            }
        }

        public void Run()
        {
            foreach (var iData in _dataChangeFilter)
            {
                ref var coinsData = ref _dataChangeFilter.Get1(iData);
                var changeAmount = _dataChangeFilter.Get2(iData).Amount;
                var count = coinsData.Count;
                ShowForAllViews(count, changeAmount);
                break;
            }
        }

        private void ShowForAllViews(int count, int? changeAmount)
        {
            foreach (var iView in _viewFilter)
            {
                CoinsCountView view = _viewFilter.Get1(iView);
                view.Show(count);
                if (changeAmount.HasValue)
                    view.ShowChange(changeAmount.Value);
            }
        }
    }
}