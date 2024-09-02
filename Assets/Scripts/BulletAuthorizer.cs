using Unity.Entities;
using UnityEngine;


public class BulletAuthorizer : MonoBehaviour
{
    [SerializeField] private GameObject prefab;
    [SerializeField] private float spawnTime;
    [SerializeField] private float spawnRate;
    [SerializeField] private float lifeTime;

    class BulletBaker : Baker<BulletAuthorizer>
    {
        public override void Bake(BulletAuthorizer authoring)
        {
            Entity entity = GetEntity(TransformUsageFlags.Dynamic);
            
            AddComponent(entity, new Bullet
            {
                Prefab = GetEntity(authoring.prefab, 
                    TransformUsageFlags.Dynamic),
                
            });
        }
    }
}