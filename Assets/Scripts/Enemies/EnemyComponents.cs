using Unity.Collections;
using Unity.Entities;
using Unity.Mathematics;
public struct EnemySpawner : IComponentData {
    public Entity Enemy;
    public float3 SpawnPosition;
    public int SpawnCount;
    public float SpawnRate;
    public float CurrentSpawnTime;
}

public struct EnemySpawnerTag : IComponentData { }

public struct EnemyMovePoints : IComponentData {
    public FixedList128Bytes<float3> points;
}

public enum WayPoints {
    Fist = 0,
    Second = 1,
    Third = 2
}

public struct EnemyCurrentPoint : IComponentData {
    public WayPoints CurrentWayPoint;
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

