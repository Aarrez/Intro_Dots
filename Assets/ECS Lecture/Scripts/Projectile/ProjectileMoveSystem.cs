using ECS_Lecture.Scripts.Player;
using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

namespace ECS_Lecture.Scripts.Projectile {
    public partial struct ProjectileMoveSystem : ISystem
    {
        [BurstCompile]
        public void OnUpdate(ref SystemState state)
        {
            float deltaTime = SystemAPI.Time.DeltaTime;

            foreach (var (transform, movespeed) 
                     in SystemAPI.Query<RefRW<LocalTransform>, ProjectileMoveSpeed>())
            {
                transform.ValueRW.Position += 
                    transform.ValueRO.Up() * movespeed.Value * deltaTime;
            }
        }
    }

}
