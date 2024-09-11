using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

public partial struct BulletMoveSystem : ISystem  {
    
    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (transform, moveSpeed) 
                 in SystemAPI.Query<RefRW<LocalTransform>, RefRO<BulletMoveSpeed>>()) {
            transform.ValueRW.Position +=
                transform.ValueRO.Up() * moveSpeed.ValueRO.Value * deltaTime;
        }
    }
}

