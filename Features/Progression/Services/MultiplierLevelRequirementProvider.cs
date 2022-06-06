namespace Features.Progression.Services
{
    public class MultiplierLevelRequirementProvider : ILevelRequirementProvider
    {
        private readonly float _multiplier;

        public MultiplierLevelRequirementProvider(float multiplier) => _multiplier = multiplier;

        public float GetExperienceFor(int level) => _multiplier * (level - 1);
    }
}