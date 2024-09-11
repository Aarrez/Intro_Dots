using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemyAuthoring : MonoBehaviour {
    [SerializeField] private Transform[] waypoints = new Transform[3];
    [SerializeField] private int enemyHealth = 100;
    [SerializeField] private float enemySpeed = 10f;

    public class EnemyAuthoringBaker : Baker<EnemyAuthoring> {
        public override void Bake(EnemyAuthoring authoring) 
        {
            var ent = GetEntity(TransformUsageFlags.Dynamic);

            AddComponent<EnemyTag>(ent);
            float2x3 temp = new float2x3();
            for (int i = 0; i < authoring.waypoints.Length-1; i++) {
                temp[i] = new float2(
                    authoring.waypoints[i].position.x,
                    authoring.waypoints[i].position.y); 
            }
            AddComponent(ent, new EnemyMovePoints {
                points = temp
            });
            AddComponent(ent, new EnemyHealth {
                Value = authoring.enemyHealth
            });
            AddComponent(ent, new EnemySpeed {
                Value = authoring.enemySpeed
            });
            AddComponent(ent, new EnemyEntity {
                Prefab = GetEntity(authoring, TransformUsageFlags.Dynamic)
            });
        }
    }
}

public class EnemySpawnerAuthoring : MonoBehaviour {
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float CurrentSpawnTime;

    public class EnemySpawnerAuthoringBaker :
        Baker<EnemySpawnerAuthoring> {
        public override void Bake(EnemySpawnerAuthoring authoring) {
            var ent = GetEntity(TransformUsageFlags.WorldSpace);
            
            AddComponent(ent, new EnemySpawner {
                prefab = GetEntity(authoring.enemyPrefab, TransformUsageFlags.WorldSpace),
                spawnRate = authoring.spawnRate,
                CurrentSpawnTime = 0,
                startPosition = 
                    new float2(authoring.transform.position.x, 
                        authoring.transform.position.y)
            });
        }
    }
}