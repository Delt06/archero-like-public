using DELTation.LeoEcsExtensions.Views;
using Features.Characters.Views;
using Leopotam.Ecs;
using UnityEngine.AI;

namespace Features.AI.Views
{
    public class AiCharacterView : CharacterView
    {
        public void Construct(NavMeshAgent agent)
        {
            _agent = agent;
        }

        protected override void AddComponents(EcsEntity entity)
        {
            base.AddComponents(entity);
            entity
                .ReplaceViewBackRef(this)
                .ReplaceUnityObjectDataData(_agent)
                ;
        }

        private NavMeshAgent _agent;
    }
}