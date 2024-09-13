using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

[UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
public partial class MyInputSystem : SystemBase {
    private InputActionMapper mapper;
    private Entity player;
    private Entity spawnerEntity;
    private EnemySpawner spawner;
    
    protected override void OnCreate() {
        RequireForUpdate<MainShipTag>();
        RequireForUpdate<MainShipMoveInput>();
        mapper = new InputActionMapper();
    }

    protected override void OnStartRunning() {
        mapper.Enable();
        
        mapper.Player.Space.performed += OnShoot;

        mapper.Player.WASD.performed += OnMove;
        mapper.Player.WASD.canceled += OnMove;

        mapper.Player.SpawnKey.performed += OnSpawn;

        mapper.UI.MousePos.performed += OnMousePosChanged;
        
        player = SystemAPI.GetSingletonEntity<MainShipTag>();
        spawnerEntity = SystemAPI.GetSingletonEntity<EnemySpawnerTag>();
    }

    private void OnSpawn(InputAction.CallbackContext obj) {
        if(!SystemAPI.Exists(spawnerEntity)) return;
        spawner = SystemAPI.GetComponent<EnemySpawner>(spawnerEntity);
        EnemySpawnSystem.SpawnEnemy?.Invoke(spawner);
    }

    private void OnMove(InputAction.CallbackContext ctx) {
        SystemAPI.SetSingleton(new MainShipMoveInput {
            Value = ctx.ReadValue<Vector2>()
        });
    }

    private void OnMousePosChanged(InputAction.CallbackContext ctx) {
        Vector2 temp = Camera.main.ScreenToWorldPoint(ctx.ReadValue<Vector2>());
            
        SystemAPI.SetSingleton(new MainShipMouseInput {
            Value = temp
        });
    }
    
    private void OnShoot(InputAction.CallbackContext context) {
        if (!SystemAPI.Exists(player)) return;
        
        SystemAPI.SetComponentEnabled<FireBulletTag>(
            player, true);
    }

    protected override void OnUpdate()
    {
    }

    protected override void OnStopRunning() {
        mapper.Player.WASD.performed -= OnMove;
        mapper.Player.WASD.canceled -= OnMove;
        
        mapper.Disable();
        player = Entity.Null;
    }
}