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
    }
}

public partial struct MainShipMoveJob : IJobEntity {
    
    public float DeltaTime;

    private void Execute(ref LocalTransform transform,
        in MainShipMoveInput input,
        MainShipMoveSpeed moveSpeed,
        MainShipRotationSpeed rotSpeed) {
        transform.Position.xy += 
            input.Value * moveSpeed.Value * DeltaTime;
        if (input.Value.Equals(float2.zero)) return;
        quaternion lookRot = quaternion.LookRotation(
                new float3(0, 1, 0), 
                new float3(input.Value.x, input.Value.y, 0));
        
        transform.Rotation = math.nlerp(
            transform.Rotation, lookRot
            , rotSpeed.Value * DeltaTime);
    }
}