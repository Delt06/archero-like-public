using DELTation.LeoEcsExtensions.Composition.Di;
using DELTation.LeoEcsExtensions.Features;
using DELTation.LeoEcsExtensions.Utilities;
using Features.Cameras.Components;
using Features.Cameras.Systems;
using Leopotam.Ecs;

namespace Features.Cameras
{
    public class CamerasFeature : Feature
    {
        public override void Register(EcsSystems systems, EcsSystems physicsSystems)
        {
            systems
                .CreateAndAdd<FaceCameraSystem>()
                .ReadFromTransforms()
                .CreateAndAdd<CameraShakeOnDeathSystem>()
                .CreateAndAdd<CameraShakeOnCriticalStrikeSystem>()
                .CreateAndAdd<CameraShakeSystem>()
                .OneFrame<CameraShakeCommand>()
                ;
        }
    }
}