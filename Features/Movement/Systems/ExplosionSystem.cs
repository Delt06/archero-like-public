using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Attack.Components;
using Features.Characters.Components;
using Features.Health.Components;
using Features.Movement.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Systems
{
    public class ExplosionSystem : IEcsRunSystem
    {
        private readonly EcsFilter<ExplosionData> _explosionsFilter = default;
        private readonly EcsFilter<UnityObjectData<CharacterController>, TeamData>.Exclude<DeathTag> _characterFilter =
            default;
        private readonly EcsWorld _world = default;

        public void Run()
        {
            foreach (var iExplosion in _explosionsFilter)
            {
                ref var explosionData = ref _explosionsFilter.Get1(iExplosion);
                var pushRadiusSqr = explosionData.PushRadius * explosionData.PushRadius;
                foreach (var iCharacter in _characterFilter)
                {
                    CharacterController characterController = _characterFilter.Get1(iCharacter);
                    var characterPosition = characterController.transform.position;
                    var offsetTowardsCharacterXZ = characterPosition - explosionData.Center;
                    offsetTowardsCharacterXZ.y = 0f;
                    var sqrDistanceXZ = offsetTowardsCharacterXZ.sqrMagnitude;
                    if (sqrDistanceXZ >= pushRadiusSqr) continue;

                    const float zeroThreshold = 0.01f;
                    if (sqrDistanceXZ <= zeroThreshold)
                    {
                        var randomAngle = Random.Range(0f, 360f);
                        offsetTowardsCharacterXZ = Quaternion.Euler(0f, randomAngle, 0f) * Vector3.forward;
                    }

                    ref var teamData = ref _characterFilter.Get2(iCharacter);
                    if (teamData.Team == explosionData.Team) continue;

                    var motionDirection = offsetTowardsCharacterXZ.normalized;
                    var motionMagnitude = explosionData.PushRadius - offsetTowardsCharacterXZ.magnitude;
                    var motion = motionDirection * motionMagnitude;
                    characterController.Move(motion);
                    _characterFilter.GetEntity(iCharacter).RequirePositionRead();

                    if (explosionData.Damage.HasValue)
                    {
                        var takeDamageEntity = _world.NewEntity();
                        ref var takeDamageCommand = ref takeDamageEntity.Get<TakeDamageCommand>();
                        takeDamageCommand.Damage = explosionData.Damage.Value;
                        takeDamageCommand.Target = _characterFilter.GetEntity(iCharacter);
                        takeDamageCommand.SourceTeam = explosionData.Team;
                    }
                }
            }
        }
    }
}