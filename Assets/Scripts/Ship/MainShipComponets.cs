using Unity.Entities;
using Unity.Mathematics;

public struct MainShipTag : IComponentData{}

public struct MainShipMoveSpeed : IComponentData {
    public float Value;
}

public struct MainShipRotationSpeed : IComponentData {
    public float Value;
}

public struct MainShipMoveInput : IComponentData {
    public float2 Value;
}
