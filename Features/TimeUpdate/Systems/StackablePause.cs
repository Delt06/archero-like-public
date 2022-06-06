using Features.TimeUpdate.Services;
using UnityEngine;

namespace Features.TimeUpdate.Systems
{
    public class StackablePause : MonoBehaviour, IPause
    {
        public void RequestPause() => Request(true);

        public void RequestResume() => Request(false);

        private void Request(bool on)
        {
            if (on)
                _stackedPauses++;
            else
                _stackedPauses--;

            _stackedPauses = Mathf.Max(0, _stackedPauses);
            Time.timeScale = _stackedPauses > 0 ? 0f : 1f;
        }

        private int _stackedPauses;
    }
}