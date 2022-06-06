using DELTation.LeoEcsExtensions.Components;
using Features.Health.Components;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Health.Systems
{
    public class CharacterControllerDeathToggleSystem : IEcsRunSystem
    {
        private readonly EcsFilter<UnityObjectData<CharacterController>, HealthData> _filter = default;

        public void Run()
        {
            foreach (var i in _filter)
            {
                CharacterController characterController = _filter.Get1(i);
                var healthData = _filter.Get2(i);

                if (healthData.IsAlive != characterController.enabled)
                    characterController.enabled = healthData.IsAlive;
            }
        }
    }
}