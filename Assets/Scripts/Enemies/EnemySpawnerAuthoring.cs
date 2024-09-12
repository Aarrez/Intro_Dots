using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public class EnemySpawnerAuthoring : MonoBehaviour {
    [SerializeField] private GameObject enemyPrefab;
    [SerializeField] private float spawnRate = 2f;
    [SerializeField] private float CurrentSpawnTime; 
    [SerializeField] private int spawnCount = 3;
    
    public class EnemySpawnerAuthoringBaker :
        Baker<EnemySpawnerAuthoring> {
        public override void Bake(EnemySpawnerAuthoring authoring) {
            var ent = GetEntity(TransformUsageFlags.WorldSpace);
            
            AddComponent(ent, new EnemySpawner {
                Enemy = GetEntity(authoring.enemyPrefab, TransformUsageFlags.WorldSpace),
                SpawnRate = authoring.spawnRate,
                CurrentSpawnTime = 0,
                SpawnCount = authoring.spawnCount
            });
            SetComponentEnabled<EnemySpawner>(ent, false);
            
            AddComponent<EnemySpawnerTag>(ent);
        }
    }
}