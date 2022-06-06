using Leopotam.Ecs;

namespace Features.Progression.Services.Session
{
    public interface ISessionPersistenceHandler
    {
        void Load(in EcsEntity persistentEntity, in EcsEntity runtimeEntity);
        void Save(in EcsEntity persistentEntity, in EcsEntity runtimeEntity);
    }
}