using Unity.Entities;
using Unity.Mathematics;

namespace ECS_Lecture.Scripts {
    public struct Spawner : IComponentData
    {
        public Entity Prefab;
        public float2 SpawnPosition;
        public float NextSpawnTime;
        public float SpawnRate;
    }

}

