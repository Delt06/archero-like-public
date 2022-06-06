using DELTation.LeoEcsExtensions.Views;
using Leopotam.Ecs;
using UnityEngine;

namespace Features.Characters.Views
{
    public class CharacterView : EntityView
    {
        public void Construct(Animator animator, CharacterController characterController)
        {
            _animator = animator;
            _characterController = characterController;
        }

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity
                .ReplaceViewBackRef(this)
                .ReplaceUnityObjectDataData(_animator)
                .ReplaceUnityObjectDataData(_characterController)
                ;
        }

        private Animator _animator;
        private CharacterController _characterController;
    }
}