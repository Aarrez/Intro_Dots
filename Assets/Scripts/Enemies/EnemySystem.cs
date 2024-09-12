using Unity.Entities;
using Unity.Transforms;

public partial struct EnemyMoveSystem : ISystem{
    public void OnUpdate(ref SystemState state) {
        float deltaTime = SystemAPI.Time.DeltaTime;
        /*new EnemyMoveJob
        {
            DeltaTime = deltaTime
        }.Schedule();*/
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
        ref LocalTransform transform,
        in EnemyMovePoints points,
        EnemySpeed speed) {
    }
}