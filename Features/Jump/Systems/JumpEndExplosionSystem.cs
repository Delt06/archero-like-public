using DELTation.LeoEcsExtensions.Components;
using Features.Characters.Components;
using Features.Jump.Components;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Jump.Systems
{
    public class JumpEndExplosionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Position, JumpExplosionData, TeamData, JumpEndEvent> _filter =
            default;
        private readonly EcsWorld _world = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                ref var position = ref _filter.Get1(i);
                ref var jumpExplosionData = ref _filter.Get2(i);
                ref var teamData = ref _filter.Get3(i);

                var explosionEntity = _world.NewEntity();
                ref var explosionData = ref explosionEntity.Get<ExplosionData>();
                explosionData.Center = position.WorldPosition;
                explosionData.PushRadius = jumpExplosionData.PushRadius;
                explosionData.Damage = jumpExplosionData.Damage;
                explosionData.Team = teamData.Team;
            }
        }
    }
}