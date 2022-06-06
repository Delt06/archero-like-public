using DELTation.LeoEcsExtensions.Utilities;
using Leopotam.Ecs;

namespace Features.Progression.Services.Session
{
    public class SessionPersistenceHandler<T> : ISessionPersistenceHandler where T : struct
    {
        public void Load(in EcsEntity persistentEntity, in EcsEntity runtimeEntity)
        {
            TransferIfHas(persistentEntity, runtimeEntity);
        }

        public void Save(in EcsEntity persistentEntity, in EcsEntity runtimeEntity)
        {
            TransferIfHas(runtimeEntity, persistentEntity);
        }

        private static void TransferIfHas(in EcsEntity source, in EcsEntity destination)
        {
            if (source.TryGet<T>(out var component))
                destination.Get<T>() = component;
        }
    }
}