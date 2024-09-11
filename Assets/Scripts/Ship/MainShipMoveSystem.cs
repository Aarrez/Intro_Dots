using Unity.Burst;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;

[UpdateBefore(typeof(TransformSystemGroup))]
public partial struct MainShipMoveSystem : ISystem {

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        float deltaTime = SystemAPI.Time.DeltaTime;

        new MainShipMoveJob {
            DeltaTime = deltaTime
        }.Schedule();
        new MainShipRotateJob {
            DeltaTime = deltaTime
        }.Schedule();
    }
}

[BurstCompile]
public partial struct MainShipRotateJob : IJobEntity {
    
    public float DeltaTime;

    [BurstCompile]
    private void Execute(ref LocalTransform transform,
        in MainShipMouseInput mouseInput,
        MainShipRotationSpeed speed) {
        
        if (mouseInput.Value.Equals(float2.zero)) return;
        
        float3 temp = new float3(mouseInput.Value.x, mouseInput.Value.y/2, 0);
        float3 direction = temp - transform.Position;
        quaternion lookRot = 
            quaternion.LookRotation(
                transform.Forward(), 
                direction);
        
        transform.Rotation = math.nlerp(
            transform.Rotation, lookRot
            , speed.Value * DeltaTime);
    }
}

[BurstCompile]
public partial struct MainShipMoveJob : IJobEntity {
    
    public float DeltaTime;

    [BurstCompile]
    private void Execute(ref LocalTransform transform,
        in MainShipMoveInput input,
        MainShipMoveSpeed moveSpeed) {
        
        transform.Position.xy += 
            input.Value * moveSpeed.Value * DeltaTime;
    }
}