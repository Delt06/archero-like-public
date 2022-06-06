using UnityEngine;

namespace Features.Loot.Services
{
    public interface ILootLocationGenerator
    {
        (Vector3 position, Quaternion rotation) GetLocation(Vector3 position);
    }
}