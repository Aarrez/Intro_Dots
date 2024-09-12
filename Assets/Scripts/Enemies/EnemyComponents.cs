using Unity.Entities;
using Unity.Mathematics;
public struct EnemySpawner : IComponentData, IEnableableComponent {
    public Entity Enemy;
    public int SpawnCount;
    public float SpawnRate;
    public float CurrentSpawnTime;
}

public struct EnemySpawnerTag : IComponentData { }

public struct EnemyMovePoints : IComponentData {
    public float2x3 points;
}

public struct EnemyEntity : IComponentData {
    public Entity Prefab;
}

public struct EnemyTag : IComponentData , IEnableableComponent{}

public struct EnemySpeed : IComponentData {
    public float Value;
}

public struct EnemyHealth : IComponentData {
    public int Value;
}

