using Unity.Entities;

public struct FireBulletTag : 
    IComponentData, IEnableableComponent { }

public struct BulletPrefab : IComponentData
{
    public Entity Value;
}

public struct BulletMoveSpeed : IComponentData {
    public float Value;
}