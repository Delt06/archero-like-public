using DELTation.LeoEcsExtensions.Components;
using Features.Attack.Views;
using Features.Characters.Components;
using Features.Movement.Components;
using Leopotam.Ecs;

namespace Features.Attack.Systems
{
    public class AttackTargetIndicatorSystem : IEcsRunSystem
    {
        private readonly EcsFilter<AttackTarget, PlayerTag> _playerWithTargetFilter = default;
        private readonly EcsFilter<ViewBackRef<AttackTargetIndicatorView>> _viewsFilter = default;

        public void Run()
        {
            foreach (var iView in _viewsFilter)
            {
                AttackTargetIndicatorView view = _viewsFilter.Get1(iView);
                if (TryGetPlayersTarget(out var target))
                    view.ShowAbove(target);
                else
                    view.Hide();
            }
        }

        private bool TryGetPlayersTarget(out EcsEntity target)
        {
            foreach (var i in _playerWithTargetFilter)
            {
                ref var attackTarget = ref _playerWithTargetFilter.Get1(i);
                target = attackTarget.Target;
                return true;
            }

            target = default;
            return false;
        }
    }
}