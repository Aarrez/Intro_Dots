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
            /*var lifetime = SystemAPI.GetComponent<BulletLifeTime>(newBullet);*/
            /*_ = DestroyBullets(newBullet, ecb, 10f);*/
            // TODO Look into Add components to an entity in the unity docs to se how to add BulletLifetime component to newBullet entity
        }
        ecb.Playback(state.EntityManager);
        ecb.Dispose();
    }
    
    private async UniTaskVoid DestroyBullets
        (float lifeTime) {
        while (true) {
            lifeTime -= Time.fixedDeltaTime;
            await UniTask.Yield(PlayerLoopTiming.Update);
            if (lifeTime <= 0) break;
        }
    }
}

public partial struct BulletLifeTimeJob : IJobEntity {
    public EntityCommandBuffer ecb;

    private void Execute(ref BulletLifeTime lifeTime) {
        
    }

}