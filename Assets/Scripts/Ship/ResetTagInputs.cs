using Unity.Collections;
using Unity.Entities;

[UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
[UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
[RequireMatchingQueriesForUpdate]
public partial struct ResetTagInputs : ISystem {
    public void OnUpdate(ref SystemState state) {
        
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        
        foreach (var (tag, entity) in 
                 SystemAPI.Query<FireBulletTag>().WithEntityAccess()) {
            ecb.SetComponentEnabled<FireBulletTag>(entity, false);
        }

        foreach (var (tag, entity) in
                 SystemAPI.Query<EnemySpawnerTag>().WithEntityAccess()) {
            ecb.SetComponentEnabled<EnemySpawner>(entity, false);
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}