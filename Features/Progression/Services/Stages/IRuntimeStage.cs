namespace Features.Progression.Services.Stages
{
    public interface IRuntimeStage
    {
        int Index { get; }
        void Pass();
    }
}