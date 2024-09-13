using Unity.Entities;
using Unity.Mathematics;
using Unity.Transforms;
using UnityEngine;

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



public partial struct EnemyMoveJob : IJobEntity {
    
    public float DeltaTime;
    //TODO Make the Enemy move in between the EnemyMovePoints
    private void Execute(
        ref LocalTransform localTransform,
        in EnemyMovePoints movePoints,
        in EnemyCurrentPoint currentPoint,
        EnemySpeed enemySpeed) {
        var dir = movePoints.points[(int)currentPoint.CurrentWayPoint] - localTransform.Position;
        math.normalize(dir);
        localTransform.Position +=  dir * enemySpeed.Value * DeltaTime ;

    }
}