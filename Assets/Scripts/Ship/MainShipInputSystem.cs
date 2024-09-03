using ECS_Lecture.Scripts.Player;
using Unity.Entities;
using UnityEngine;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class MainShipInputSystem : SystemBase {
    private InputActionMapper mapper;
    private Vector2 movementInput;
    private Entity player;
    

    protected override void OnCreate() {
       
       
        RequireForUpdate<MainShipTag>();
        RequireForUpdate<PlayerMoveInput>();
        mapper = new InputActionMapper();
    }

    protected override void OnStartRunning() {
        mapper.Enable();
        
        mapper.Player.Space.performed += _ => OnShoot();
        mapper.Player.WASD.performed += ctx => {
            movementInput = ctx.ReadValue<Vector2>();
        };
        mapper.Player.WASD.canceled += ctx => {
            movementInput = ctx.ReadValue<Vector2>();
        };
        player = SystemAPI.GetSingletonEntity<PlayerTag>();
    }
    private void OnShoot() {
        if (!SystemAPI.Exists(player)) return;
        
        SystemAPI.SetComponentEnabled<FireBulletTag>(
            player, true);
    }

    protected override void OnUpdate() {
        SystemAPI.SetSingleton(new PlayerMoveInput {
            Value = movementInput
        });
    }

    protected override void OnStopRunning() {
        mapper.Disable();
        player = Entity.Null;
    }
}