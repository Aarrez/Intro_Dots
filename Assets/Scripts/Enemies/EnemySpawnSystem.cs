using Unity.Burst;
using Unity.Collections;
using Unity.Entities;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial struct EnemySpawnSystem : ISystem {

    [BurstCompile]
    public void OnUpdate(ref SystemState state) {
        foreach (var spawner in SystemAPI.Query<EnemySpawner>().WithAll<EnemySpawnerTag>()) {
            new EnemySpawnJob {
                Spawner = spawner,
                DeltaTime = SystemAPI.Time.DeltaTime,
                Manager = state.EntityManager
            }.ScheduleParallel();
        }
    }
} 

[BurstCompile]
public partial struct EnemySpawnJob : IJobEntity {
    public float DeltaTime;
    public EnemySpawner Spawner;
    public EntityCommandBuffer Ecb;
    public Entity[] Entities;
    public EntityManager Manager;
    private void Execute() {
        Ecb = new EntityCommandBuffer(Allocator.Temp);
        
        var entity = Ecb.Instantiate(Spawner.Enemy);
        
        Ecb.Playback(Manager);
        Ecb.Dispose();
    }
}