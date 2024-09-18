using Unity.Burst;
using Unity.Entities;
using Unity.Transforms;

[RequireMatchingQueriesForUpdate]
public partial struct BulletMoveSystem : ISystem  {
    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        float deltaTime = SystemAPI.Time.DeltaTime;

        foreach (var (transform, moveSpeed, lifeTime, entity) 
                 in SystemAPI.Query<RefRW<LocalTransform>, RefRO<BulletMoveSpeed>, RefRW<BulletLifeTime>>().WithAny<BulletTag>().WithEntityAccess()) {
            transform.ValueRW.Position +=
                transform.ValueRO.Up() * moveSpeed.ValueRO.Value * deltaTime;
            if (lifeTime.ValueRO.Current > 0) {
                lifeTime.ValueRW.Current -= deltaTime;
                continue;
            }
            
            state.EntityManager.SetComponentEnabled<BulletTag>(entity, false);
        }
    }
}

