using DELTation.LeoEcsExtensions.Components;
using Features.Characters;
using Features.Characters.Components;
using Features.Health.Components;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Systems
{
    public class FindTargetSystem : IEcsRunSystem
    {
        private readonly EcsFilter<Position, TeamData, FindTargetData, HealthData> _filter = default;
        private readonly EcsFilter<Position, TeamData, HealthData> _targetsFilter = default;

        public void Run()
        {
            foreach (var index in _filter)
            {
                var entity = _filter.GetEntity(index);
                ref var healthData = ref _filter.Get4(index);

                if (!healthData.IsAlive)
                {
                    entity.Del<AttackTarget>();
                    continue;
                }

                var position = _filter.Get1(index).WorldPosition;
                var team = _filter.Get2(index).Team;
                var findTargetData = _filter.Get3(index);
                var maxSqrDistance = findTargetData.MaxDistance * findTargetData.MaxDistance;

                var closestTargetIndex = GetClosestTargetIndex(team, position, maxSqrDistance);

                if (closestTargetIndex == null)
                {
                    entity.Del<AttackTarget>();
                }
                else
                {
                    var target = _targetsFilter.GetEntity(closestTargetIndex.Value);
                    entity.Replace(new AttackTarget { Target = target });
                }
            }
        }

        private int? GetClosestTargetIndex(Team team, Vector3 position, float maxSqrDistance)
        {
            int? closestTargetIndex = null;
            var minSqrDistance = Mathf.Infinity;

            foreach (var targetIndex in _targetsFilter)
            {
                var targetTeam = _targetsFilter.Get2(targetIndex).Team;
                if (team == targetTeam) continue;

                ref var healthData = ref _targetsFilter.Get3(targetIndex);
                if (!healthData.IsAlive) continue;

                var targetPosition = _targetsFilter.Get1(targetIndex).WorldPosition;
                var offset = targetPosition - position;
                offset.y = 0f;
                var sqrDistance = offset.sqrMagnitude;
                if (sqrDistance >= minSqrDistance) continue;
                if (sqrDistance > maxSqrDistance) continue;

                closestTargetIndex = targetIndex;
                minSqrDistance = sqrDistance;
            }

            return closestTargetIndex;
        }
    }
}