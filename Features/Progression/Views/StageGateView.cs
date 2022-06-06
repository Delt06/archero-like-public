using DELTation.LeoEcsExtensions.Views;
using DG.Tweening;
using DOTweenExtensions;
using Features.Movement.Components;
using Features.Progression.Components;
using Leopotam.Ecs;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Progression.Views
{
    public class StageGateView : EntityView
    {
        [SerializeField] [Min(0f)] private float _openDelay = 1f;
        [SerializeField] [Required] private Transform _explosionPosition = default;
        [SerializeField] [Min(0f)] private float _explosionPushRadius = 1f;
        [SerializeField] [Required] private GameObject _highlight = default;
        [SerializeField] [Required] private Transform _leftDoor = default;
        [SerializeField] [Required] private Transform _rightDoor = default;
        [SerializeField] private float _openAngle = 130f;
        [SerializeField] [Min(0f)] private float _openDuration = 0.5f;
        [SerializeField] private Ease _openEase = Ease.OutElastic;
        [SerializeField] [Min(0f)] private float _openOvershoot = 1.7f;

        private readonly ControlledSequence _controlledSequence = new ControlledSequence();
        private bool _allowEntering;

        private void OnTriggerEnter(Collider other)
        {
            if (!_allowEntering) return;
            if (!other.attachedRigidbody) return;
            if (!other.attachedRigidbody.TryGetComponent(out IEntityView otherView)) return;
            if (!otherView.TryGetEntity(out var otherEntity)) return;

            otherEntity.Get<EnteredStageGateEvent>();
        }

        public void Open() =>
            _controlledSequence.RecreateSequence()
                .AppendInterval(_openDelay)
                .AppendCallback(StartOpening);

        private void StartOpening()
        {
            RotateDoor(_leftDoor, _openAngle);
            RotateDoor(_rightDoor, -_openAngle);
            _highlight.SetActive(true);
            _allowEntering = true;

            OnOpened();
        }

        private void RotateDoor(Transform door, float angle)
        {
            var rotation = new Vector3(0f, angle, 0f);
            door.DOLocalRotate(rotation, _openDuration)
                .SetEase(_openEase, _openOvershoot);
        }

        private void OnOpened()
        {
            var eventEntity = World.NewEntity();
            ref var explosionData = ref eventEntity.Get<ExplosionData>();
            explosionData.Center = _explosionPosition.position;
            explosionData.PushRadius = _explosionPushRadius;
        }

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity.ReplaceViewBackRef(this);
        }

        protected override void OnDestroyed()
        {
            base.OnDestroyed();
            _leftDoor.DOKill();
            _rightDoor.DOKill();
            _controlledSequence?.Kill();
        }

        private void OnDrawGizmos()
        {
            if (_explosionPosition)
            {
                Gizmos.color = Color.red;
                Gizmos.DrawWireSphere(_explosionPosition.position, _explosionPushRadius);
            }
        }
    }
}