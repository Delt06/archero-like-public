using Features.TimeUpdate.Services;
using Leopotam.Ecs;

namespace Features.TimeUpdate.Systems
{
    public class UpdateTimeSystem : IEcsRunSystem
    {
        private readonly ModifiableTime _time;

        public UpdateTimeSystem(ModifiableTime time) => _time = time;

        public void Run()
        {
            _time.DeltaTime = UnityEngine.Time.deltaTime;
        }
    }
}