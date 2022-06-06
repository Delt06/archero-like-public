using DELTation.LeoEcsExtensions.Components;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Cameras.Components;
using Leopotam.Ecs;
using UnityEngine;
using UnityEngine.Assertions;

namespace Features.Cameras.Systems
{
    public class FaceCameraSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<UnityObjectData<Transform>, FaceCameraTag> _filter = default;
        private Transform _cameraTransform;

        public void Init()
        {
            var camera = Camera.main;
            Assert.IsNotNull(camera);
            _cameraTransform = camera.transform;
        }

        public void Run()
        {
            foreach (var i in _filter)
            {
                Transform transform = _filter.Get1(i);
                transform.rotation = _cameraTransform.rotation;
                _filter.GetEntity(i).RequireRotationRead();
            }
        }
    }
}