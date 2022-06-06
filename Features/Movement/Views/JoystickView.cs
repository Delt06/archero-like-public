using DELTation.LeoEcsExtensions.Views;
using Features.Movement.Behaviours;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Movement.Views
{
    public class JoystickView : EntityView
    {
        private Joystick _joystick;

        public void Construct(Joystick joystick)
        {
            _joystick = joystick;
        }

        public Vector2 Value => _joystick.Value;

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity.ReplaceViewBackRef(this);
        }
    }
}