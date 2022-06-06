using System;
using DG.Tweening;
using JetBrains.Annotations;

namespace DOTweenExtensions
{
    public class ControlledSequence
    {
        private readonly TweenCallback _onComplete;
        private Sequence _sequence;

        public ControlledSequence([CanBeNull] Action onComplete = null)
        {
            if (onComplete == null)
                _onComplete = () => _sequence = null;
            else
                _onComplete = () =>
                {
                    onComplete();
                    _sequence = null;
                };
        }

        public ControlledSequence CompleteIfExists()
        {
            if (TryGetSequence(out var sequence))
                sequence.Complete();
            return this;
        }

        public bool TryGetSequence(out Sequence sequence)
        {
            sequence = _sequence;
            return _sequence != null;
        }

        public Sequence GetOrCreateSequence() => _sequence ??= DOTween.Sequence().OnComplete(_onComplete);

        public Sequence RecreateSequence()
        {
            Kill();
            return GetOrCreateSequence();
        }

        public ControlledSequence Kill()
        {
            _sequence?.Kill();
            _sequence = null;
            return this;
        }
    }
}