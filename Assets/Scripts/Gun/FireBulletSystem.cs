using Cysharp.Threading.Tasks;
using Unity.Collections;
using Unity.Entities;
using Unity.Transforms;
using UnityEngine;

[UpdateInGroup(typeof(SimulationSystemGroup))]
[UpdateBefore(typeof(TransformSystemGroup))]
[RequireMatchingQueriesForUpdate]
public partial struct FireBulletSystem : ISystem {
    
    public void OnUpdate(ref SystemState state) {
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        foreach (var (bulletPrefab, transform) 
                 in SystemAPI.Query<BulletEntity, LocalTransform>().WithAll<FireBulletTag>()) {
            var newBullet = ecb.Instantiate(bulletPrefab.Value);
            var bulletTransform = 
                LocalTransform.FromPositionRotation(transform.Position, transform.Rotation);
            
            ecb.SetComponent(newBullet, bulletTransform);
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}



[UpdateInGroup(typeof(SimulationSystemGroup), OrderLast = true)]
[RequireMatchingQueriesForUpdate]
public partial struct BulletLifeTimeSystem : ISystem {

    private float currentTime;
    public void OnUpdate(ref SystemState state) {
        var ecb = new EntityCommandBuffer(Allocator.Temp);
        foreach (var (tag, entity) in 
                 SystemAPI.Query<RefRW<BulletTag>>().WithDisabled<BulletTag>().WithEntityAccess()) {
            ecb.DestroyEntity(entity); 
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
}