

using Unity.Collections;
using Unity.Entities;

[UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
[UpdateAfter(typeof(EndSimulationEntityCommandBufferSystem))]
public partial struct ResetBulletInput : ISystem {

    public void OnUpdate(ref SystemState state) {
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        foreach (var (tag, entity) in 
                 SystemAPI.Query<FireBulletTag>().WithEntityAccess()) {
            ecb.SetComponentEnabled<FireBulletTag>(entity, false);
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}