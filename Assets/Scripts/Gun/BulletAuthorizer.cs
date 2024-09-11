using Unity.Entities;
using UnityEngine;

public class BulletAuthorizer : MonoBehaviour {
    
    [SerializeField] private float speed = 100f;
    [SerializeField] private float lifeTime = 2f;
    [SerializeField] private int damage = 1;

    public class BulletAuthorizerBaker : Baker<BulletAuthorizer> {
        public override void Bake(BulletAuthorizer authoring) {
            
            Entity ent = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent<FireBulletTag>(ent);

            AddComponent(ent, new BulletMoveSpeed {
                Value = authoring.speed
            });
            AddComponent(ent, new BulletDamage {
                Value = authoring.damage
            });
            AddComponent(ent, new BulletLifeTime {
                Value = authoring.lifeTime
            });
            
            AddComponent(ent, new BulletEntity{
                Value = GetEntity(authoring, TransformUsageFlags.Dynamic)
            });
        }
    }
    
}