using Features.Loot.Services;
using Sirenix.OdinInspector;
using UnityEngine;

namespace Features.Loot.Behaviour
{
    public class RandomLootLocationGenerator : MonoBehaviour, ILootLocationGenerator
    {
        [SerializeField] [MinMaxSlider(0f, 5f, true)]
        private Vector2 _distanceRange = new Vector2(0f, 1f);

        public (Vector3 position, Quaternion rotation) GetLocation(Vector3 position)
        {
            var distance = Random.Range(_distanceRange.x, _distanceRange.y);
            position += Quaternion.Euler(0f, Random.Range(0f, 360f), 0f) * Vector3.forward * distance;
            var rotation = Quaternion.Euler(0f, Random.Range(0f, 360f), 0f);
            return (position, rotation);
        }
    }
}