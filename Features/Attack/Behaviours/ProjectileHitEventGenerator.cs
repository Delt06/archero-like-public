using Features.Attack.Components.Projectiles;
using Features.Attack.Views.Projectiles;
using Features.Characters.Views;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Attack.Behaviours
{
    public class ProjectileHitEventGenerator : MonoBehaviour
    {
        public void Construct(ProjectileView projectileView)
        {
            _projectileView = projectileView;
        }

        private void OnTriggerEnter(Collider other)
        {
            if (!other.attachedRigidbody) return;
            if (!other.attachedRigidbody.TryGetComponent(out CharacterView otherCharacterView)) return;
            if (!otherCharacterView.TryGetEntity(out var otherCharacterEntity)) return;
            if (!_projectileView.TryGetEntity(out var projectileEntity)) return;

            var collisionEvent = new ProjectileHitEvent
            {
                Target = otherCharacterEntity,
            };
            projectileEntity.Get<ProjectileHitEvent>() = collisionEvent;
        }

        private ProjectileView _projectileView;
    }
}