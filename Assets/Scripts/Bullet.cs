using Unity.Entities;
using Unity.Mathematics;
using UnityEngine;

public struct Bullet : IComponentData
{
    public Entity Prefab;
    public float2 SpawnPosition;
    public Quaternion Rotation;
    public float SpawnTime;
    public float SpawnRate;
    public float Lifetime;
}