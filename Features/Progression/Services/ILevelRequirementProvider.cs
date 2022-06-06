namespace Features.Progression.Services
{
    public interface ILevelRequirementProvider
    {
        float GetExperienceFor(int level);
    }
}