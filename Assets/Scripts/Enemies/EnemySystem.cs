using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
public partial struct EnemyMoveSystem : ISystem{
    public void OnUpdate(ref SystemState state) {
        new EnemyMoveJob {
            DeltaTime = SystemAPI.Time.DeltaTime,
        }.Schedule();
    }
}

public partial struct EnemyDamageSystem : ISystem {
    /*
     * TODO Find out how to call this struct to execute either a job or something else
     */
}

public partial struct EnemyDestroySystem : ISystem {
    // TODO Keep track of enemy life time and destroy when 0
}


[BurstCompile]
public partial struct EnemyMoveJob : IJobEntity {
    
    public float DeltaTime;
    private void Execute(
        ref LocalTransform localTransform,
        in EnemyMovePoints movePoints,
        ref EnemyCurrentPoint currentPoint,
        EnemySpeed enemySpeed) {
        
        float3 point = movePoints.points[(int)currentPoint.CurrentWayPoint];
        float3 closeToPoint = point * 0.9f;
        bool3 variable = localTransform.Position >= closeToPoint;
        if (variable is { x: true, y: true }) {
            currentPoint.CurrentWayPoint++;
            point = movePoints.points[(int)currentPoint.CurrentWayPoint];
            if ((int)currentPoint.CurrentWayPoint == 2) return;
            
        }
        var dir = point - localTransform.Position;
        math.normalize(dir);
        localTransform.Position += dir * enemySpeed.Value * DeltaTime;
        
        

    }
}