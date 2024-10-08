﻿using Unity.Entities;

public struct FireBulletTag : 
    IComponentData, IEnableableComponent { }

public struct BulletTag : IComponentData, IEnableableComponent{ }

public struct BulletEntity : IComponentData {
    public Entity Value;
}

public struct BulletMoveSpeed : IComponentData {
    public float Value;
}

public struct BulletLifeTime : IComponentData {
    public float Current;
}

public struct BulletDamage : IComponentData {
    public int Value;
}