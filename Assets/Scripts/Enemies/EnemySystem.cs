using Unity.Entities;
using Unity.Transforms;

public partial struct EnemyMoveSystem : ISystem{
    public void OnUpdate(ref SystemState state) {
        float deltaTime = SystemAPI.Time.DeltaTime;
        new EnemyMoveJob
        {
            DeltaTime = deltaTime
        }.Schedule();
    }
}

public partial struct EnemyDamageSystem : ISystem {
    /*
     * TODO Find out how to call this struct to execute either a job or something else
     */
}



public partial struct EnemyMoveJob : IJobEntity {
    public float DeltaTime;

    //TODO Make the Enemy move in between the EnemyMovePoints then when reaching the last one destory
    private void Execute(
        ref LocalTransform transform,
        in EnemyMovePoints points,
        EnemySpeed speed) {
        
    }
}