using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class MainShipInputSystem : SystemBase {
    private InputActionMapper mapper;
    private Entity player;
    
    protected override void OnCreate() {
        RequireForUpdate<MainShipTag>();
        RequireForUpdate<MainShipMoveInput>();
        mapper = new InputActionMapper();
    }

    protected override void OnStartRunning() {
        mapper.Enable();
        
        mapper.Player.Space.performed += OnShoot;
        
        player = SystemAPI.GetSingletonEntity<MainShipTag>();
    }
    private void OnShoot(InputAction.CallbackContext context) {
        if (!SystemAPI.Exists(player)) return;
        
        SystemAPI.SetComponentEnabled<FireBulletTag>(
            player, true);
    }

    protected override void OnUpdate()
    {
        var movementInput = mapper.Player.WASD.ReadValue<Vector2>();
        
        SystemAPI.SetSingleton(new MainShipMoveInput() {
            Value = movementInput
        });
    }

    protected override void OnStopRunning() {
        mapper.Disable();
        player = Entity.Null;
    }
}