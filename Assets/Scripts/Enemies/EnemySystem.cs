using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
[UpdateBefore(typeof(LateSimulationSystemGroup))]
[RequireMatchingQueriesForUpdate]

public partial struct EnemyMoveSystem : ISystem {


    public void OnCreate(ref SystemState state) {
        state.RequireForUpdate<EnemyTag>();
    }

    //TODO Figure out how to destroy entites
    public void OnUpdate(ref SystemState state) {
        foreach (var (transform, movePoints, currentPoint, speed, tag, entity) in 
                 SystemAPI.Query<RefRW<LocalTransform>, RefRO<EnemyMovePoints>, RefRW<EnemyCurrentPoint>, RefRO<EnemySpeed>, RefRW<EnemyTag>>().WithEntityAccess() ) {
            float3 point = movePoints.ValueRO.points[(int)currentPoint.ValueRO.CurrentWayPoint];
            
            float3 closeToPoint = point * 0.9f;

            bool3 variable = new bool3();
            
            var pos = transform.ValueRO.Position;
            switch (currentPoint.ValueRO.CurrentWayPoint) {
                case WayPoints.Fist:
                    variable  = pos >= closeToPoint;
                    break;
                case WayPoints.Second:
                    variable.x = pos.x <= closeToPoint.x;
                    variable.y = pos.y >= closeToPoint.y;
                    break;
                case WayPoints.Third:
                    variable.x = pos.x >= closeToPoint.x;
                    variable.y = pos.y <= closeToPoint.y;
                    break;
            }
            if (variable is { x: true, y: true }) {
                currentPoint.ValueRW.CurrentWayPoint++;
                if (currentPoint.ValueRO.CurrentWayPoint == WayPoints.Complete) {
                    state.EntityManager.SetComponentEnabled<EnemyTag>(entity, false);
                    return;
                }
                point = movePoints.ValueRO.points[(int)currentPoint.ValueRO.CurrentWayPoint];
                
            }
            var dir = point - transform.ValueRO.Position;
            math.normalize(dir);
            transform.ValueRW.Position += dir * speed.ValueRO.Value * SystemAPI.Time.DeltaTime;
        }
    }
}
[UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
[UpdateAfter(typeof(LateSimulationSystemGroup))]
[RequireMatchingQueriesForUpdate]
public partial struct EnemyDestroySystem : ISystem {
    public void OnUpdate(ref SystemState state) {
        var ecb = new EntityCommandBuffer(Allocator.Persistent);
        foreach (var (tag, entity) in SystemAPI.Query<EnemyTag>().WithDisabled<EnemyTag>().WithEntityAccess()) {
            ecb.DestroyEntity(entity);
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}