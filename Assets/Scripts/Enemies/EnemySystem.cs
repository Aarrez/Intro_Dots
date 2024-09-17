using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
[RequireMatchingQueriesForUpdate]

public partial struct EnemyMoveSystem : ISystem {
    
    
    //TODO Figure out how to destroy entites
    public void OnUpdate(ref SystemState state) {
        foreach (var (transform, movePoints, currentPoint, speed, entity) in 
                 SystemAPI.Query<RefRW<LocalTransform>, RefRO<EnemyMovePoints>, RefRW<EnemyCurrentPoint>, RefRO<EnemySpeed>>().WithEntityAccess()) {
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
                    DestroyEnemy(entity, ref state);
                    return;
                }
                point = movePoints.ValueRO.points[(int)currentPoint.ValueRO.CurrentWayPoint];
                
            }
            var dir = point - transform.ValueRO.Position;
            math.normalize(dir);
            transform.ValueRW.Position += dir * speed.ValueRO.Value * SystemAPI.Time.DeltaTime;
        }
    }

    private void DestroyEnemy(Entity entity, ref SystemState state) {
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        ecb.DestroyEntity(entity);
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
        
        
    }
}