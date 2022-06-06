using Leopotam.Ecs;

namespace Features.Progression.Services.Session
{
    public interface ISessionProgress
    {
        void OnPassedStage(int stageIndex);
        int? LastPassedStageIndex { get; }
        void ResetAll();
        void LoadInto(in EcsEntity entity);
        void SaveFrom(in EcsEntity entity);
    }
}