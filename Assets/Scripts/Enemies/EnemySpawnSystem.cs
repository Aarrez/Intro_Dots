using Unity.Burst;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine.Events;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial struct EnemySpawnSystem : ISystem {
    private EntityManager entityManager;
    public static UnityAction<EnemySpawner> SpawnEnemy;

    public void OnCreate(ref SystemState state) {
        SpawnEnemy += OnSpawnEnemy;
    }

    private void OnSpawnEnemy(EnemySpawner spawner) {
        var world = World.DefaultGameObjectInjectionWorld;
        entityManager = world.EntityManager;
        var Ecb = new EntityCommandBuffer(Allocator.Temp);
        
        var ent = Ecb.Instantiate(spawner.Enemy);
        Ecb.SetComponent(ent, 
            LocalTransform.FromPosition(spawner.SpawnPosition));
        
        Ecb.Playback(entityManager);
        Ecb.Dispose();
    }

    public void OnDestroy(ref SystemState state) {
        SpawnEnemy -= OnSpawnEnemy;
    }
} 

[BurstCompile]
public partial struct EnemySpawnJob : IJobEntity {
    public EnemySpawner Spawner;
    public EntityCommandBuffer Ecb;
    public Entity[] Entities;
    public EntityManager Manager;
    private void Execute() {
        
    }
}