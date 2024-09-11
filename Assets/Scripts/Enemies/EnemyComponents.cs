using Unity.Entities;
using Unity.Mathematics;
public struct EnemySpawner : IComponentData {
    public Entity prefab;
    public float2 startPosition;
    public float spawnRate;
    public float CurrentSpawnTime;
}

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

