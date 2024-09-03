using Unity.Entities;
using UnityEngine;
using UnityEngine.InputSystem;

namespace ECS_Lecture.Scripts.Player {
    [UpdateInGroup(typeof(InitializationSystemGroup), OrderLast = true)]
    public partial class PlayerInputSystem : SystemBase
    {
        private InputActionMapper mapper;
        private Entity player;

        protected override void OnCreate()
        {
            RequireForUpdate<PlayerTag>();
            RequireForUpdate<PlayerMoveInput>();
            mapper = new InputActionMapper();
        
        }

        protected override void OnStartRunning()
        {
            mapper.Enable();
            mapper.Player.Space.performed += OnShoot;
            
            player = SystemAPI.GetSingletonEntity<PlayerTag>();
        }

        private void OnShoot(InputAction.CallbackContext obj)
        {
            if(!SystemAPI.Exists(player)) return;
        
            SystemAPI.SetComponentEnabled<FireProjectileTag>(
                player, true);
        }

        protected override void OnUpdate()
        {
            Vector2 moveInput = mapper.Player.WASD.ReadValue<Vector2>();
        
            SystemAPI.SetSingleton(new PlayerMoveInput{Value = moveInput});
        }

        protected override void OnStopRunning()
        {
            mapper.Disable();
            player = Entity.Null;
        }
    }
}
