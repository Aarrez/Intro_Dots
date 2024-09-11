using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

namespace ECS_Lecture.Scripts {
    public class SpawnerAuthorization : MonoBehaviour
    {
        [SerializeField] private GameObject Prefab;
        [SerializeField] private float SpawnRate;
    
        class SpawnBaker : Baker<SpawnerAuthorization>
        {
            public override void Bake(SpawnerAuthorization authoring)
            {
                Entity entity = GetEntity(TransformUsageFlags.Dynamic);

                AddComponent(entity, new Spawner
                {
                    Prefab = GetEntity(authoring.Prefab, TransformUsageFlags.Dynamic),
                    SpawnPosition = float2.zero,
                    NextSpawnTime = 0,
                    SpawnRate = 2
                });
            }
        }
    }
}
