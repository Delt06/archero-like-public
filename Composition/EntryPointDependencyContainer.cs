using DELTation.DIFramework;
using DELTation.DIFramework.Containers;
using DELTation.LeoEcsExtensions.Composition;
using Features.Progression.Services;
using Features.TimeUpdate.Services;
using UnityEngine;

namespace Composition
{
    [RequireComponent(typeof(EcsEntryPoint))]
    public class EntryPointDependencyContainer : DependencyContainerBase
    {
        protected override void ComposeDependencies(ContainerBuilder builder)
        {
            var entryPoint = GetComponent<EcsEntryPoint>();
            builder.Register(entryPoint);
            builder.Register(new MultiplierLevelRequirementProvider(100f));
            builder.Register<ModifiableTime>();
        }
    }
}