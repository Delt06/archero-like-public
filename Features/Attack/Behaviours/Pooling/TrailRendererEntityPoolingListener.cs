using DELTation.LeoEcsExtensions.Pooling;
using UnityEngine;

namespace Features.Attack.Behaviours.Pooling
{
    [RequireComponent(typeof(TrailRenderer))]
    public class TrailRendererEntityPoolingListener : MonoBehaviour, IEntityViewPoolingListener
    {
        public void OnPreCreated()
        {
            _trailRenderer.Clear();
        }

        public void OnPreDisposed() { }

        private void Awake()
        {
            _trailRenderer = GetComponent<TrailRenderer>();
        }

        private TrailRenderer _trailRenderer;
    }
}