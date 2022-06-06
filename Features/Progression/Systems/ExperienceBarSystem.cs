using DELTation.LeoEcsExtensions.Components;
using Features.Progression.Components;
using Features.Progression.Services;
using Features.Progression.Views;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Progression.Systems
{
    public class ExperienceBarSystem : IEcsInitSystem, IEcsRunSystem
    {
        private readonly EcsFilter<ExperienceData> _allTargetsFilter = default;
        private readonly EcsFilter<ExperienceData, ExperienceIncreaseEvent> _updatedTargetsFilter = default;
        private readonly EcsFilter<ViewBackRef<ExperienceBarView>> _barsFilter = default;
        private readonly ILevelRequirementProvider _levelRequirementProvider;

        public ExperienceBarSystem(ILevelRequirementProvider levelRequirementProvider) =>
            _levelRequirementProvider = levelRequirementProvider;

        public void Init()
        {
            ResetBars();

            foreach (var iTarget in _allTargetsFilter)
            {
                ref var experienceData = ref _updatedTargetsFilter.Get1(iTarget);
                SnapBarsTo(experienceData);
            }
        }

        private void ResetBars()
        {
            foreach (var iBar in _barsFilter)
            {
                var experienceBarView = _barsFilter.Get1(iBar).View;
                experienceBarView.UpdateFillAmount(0f);
                experienceBarView.UpdateLevel(1);
            }
        }

        public void Run()
        {
            foreach (var iTarget in _updatedTargetsFilter)
            {
                ref var experienceData = ref _updatedTargetsFilter.Get1(iTarget);
                SmoothlyUpdateBars(experienceData);
            }
        }

        private void SnapBarsTo(in ExperienceData experienceData)
        {
            var level = experienceData.Level;
            var requiredExperience = _levelRequirementProvider.GetExperienceFor(level + 1);
            var experienceProgress = Mathf.Clamp01(experienceData.Experience / requiredExperience);

            foreach (var iBar in _barsFilter)
            {
                var experienceBarView = _barsFilter.Get1(iBar).View;
                SnapBarTo(experienceBarView, experienceProgress, level);
            }
        }

        private static void SnapBarTo(ExperienceBarView view, float experienceProgress, int level)
        {
            view.CompleteSequence();
            view.UpdateFillAmount(experienceProgress);
            view.UpdateLevel(level);
        }

        private void SmoothlyUpdateBars(in ExperienceData experienceData)
        {
            var level = experienceData.Level;
            var requiredExperience = _levelRequirementProvider.GetExperienceFor(level + 1);
            var experienceProgress = Mathf.Clamp01(experienceData.Experience / requiredExperience);

            foreach (var iBar in _barsFilter)
            {
                var experienceBarView = _barsFilter.Get1(iBar).View;

                if (experienceBarView.LastDisplayedLevel > level)
                    SnapBarTo(experienceBarView, experienceProgress, level);
                else
                    SmoothlyUpdateBar(experienceBarView, level, experienceProgress);
            }
        }

        private static void SmoothlyUpdateBar(ExperienceBarView view, int level, float experienceProgress)
        {
            for (var displayedLevel = view.LastDisplayedLevel + 1;
                displayedLevel <= level;
                displayedLevel++)
            {
                view.AppendUpdateFillAmountSmoothly(1f);
                view.AppendUpdateFillAmount(0f);
                view.AppendUpdateLevelNumber(displayedLevel);
            }

            view.AppendUpdateFillAmountSmoothly(experienceProgress);
        }
    }
}