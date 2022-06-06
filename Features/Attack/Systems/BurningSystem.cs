using Features.Attack.Components;
using Features.TimeUpdate.Services;
using Leopotam.Ecs;

namespace Features.Attack.Systems
{
    public class BurningSystem : IEcsRunSystem
    {
        private readonly EcsFilter<BurningData> _filter = default;
        private readonly EcsWorld _world = default;
        private readonly ITime _time;

        public BurningSystem(ITime time) => _time = time;

        public void Run()
        {
            var deltaTime = _time.DeltaTime;

            foreach (var i in _filter)
            {
                var entity = _filter.GetEntity(i);
                ref var burningData = ref _filter.Get1(i);
                if (burningData.Target.IsNull())
                {
                    entity.Del<BurningData>();
                }
                else
                {
                    burningData.TimeTillNextDamage -= deltaTime;

                    if (burningData.TimeTillNextDamage <= 0f)
                    {
                        var takeDamageCommandEntity = _world.NewEntity();
                        ref var takeDamageCommand = ref takeDamageCommandEntity.Get<TakeDamageCommand>();
                        takeDamageCommand.Damage = burningData.Damage;
                        takeDamageCommand.Target = burningData.Target;
                        takeDamageCommand.SourceTeam = null;
                        burningData.TimeTillNextDamage = burningData.Period;
                    }

                    burningData.RemainingTime -= deltaTime;

                    if (burningData.RemainingTime <= 0f) entity.Del<BurningData>();
                }
            }
        }
    }
}