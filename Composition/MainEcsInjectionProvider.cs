using System;
using DELTation.DIFramework;
using DELTation.LeoEcsExtensions.Composition;
using Leopotam.Ecs;
using UnityEngine;

namespace Composition
{
    public sealed class MainEcsInjectionProvider : MonoBehaviour, IEcsInjectionProvider
    {
        public void Inject(EcsSystems systems, EcsSystems physicsSystems)
        {
            if (systems == null) throw new ArgumentNullException(nameof(systems));
            if (physicsSystems == null) throw new ArgumentNullException(nameof(physicsSystems));

            var allRegisteredObjects = Di.GetAllRegisteredObjects();

            foreach (var registeredObject in allRegisteredObjects)
            {
                systems.Inject(registeredObject);
            }
        }
    }
}