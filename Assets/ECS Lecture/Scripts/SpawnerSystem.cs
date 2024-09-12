using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

namespace ECS_Lecture.Scripts {
    public partial struct SpawnerSystem : ISystem
    {
        public void OnUpdate(ref SystemState state)
        {
            foreach (RefRW<Spawner> spawner in SystemAPI.Query<RefRW<Spawner>>())
            {
                if (spawner.ValueRO.NextSpawnTime < SystemAPI.Time.ElapsedTime)
                {
                    Entity newEntity = state.EntityManager.Instantiate(spawner.ValueRO.Prefab);
                    float3 pos = new float3(spawner.ValueRO.SpawnPosition.x, spawner.ValueRO.SpawnPosition.y, 0);
                    state.EntityManager.SetComponentData(newEntity, LocalTransform.FromPosition(pos));
                    spawner.ValueRW.NextSpawnTime = (float)SystemAPI.Time.ElapsedTime + spawner.ValueRO.SpawnRate;
                }
            }
        }
    }
}
