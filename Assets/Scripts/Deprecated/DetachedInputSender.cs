/*using Unity.Entities;
using UnityEngine.Events;
using UnityEngine.InputSystem;

internal class InputCreator
{
    public InputActionMapper mapper;
    public InputCreator()
    {
        mapper = new InputActionMapper();
    }

    ~InputCreator()
    {
        mapper.Disable();
        mapper.Dispose();
    }
}

[UpdateInGroup(typeof(InitializationSystemGroup))]
[UpdateAfter(typeof(EndInitializationEntityCommandBufferSystem))]
public partial class DetachedInputSender : SystemBase
{
    private static InputCreator inputCreator;
    public static UnityAction<InputAction.CallbackContext> WasdInputAction;
    public static UnityAction SpaceInputAction;

    protected override void OnStartRunning() {
        inputCreator = new InputCreator();
        inputCreator.mapper.Enable();
        
        inputCreator.mapper.Player.WASD.performed += ctx => WasdInputAction?.Invoke(ctx);
        inputCreator.mapper.Player.WASD.canceled += arg0 => WasdInputAction?.Invoke(arg0);
        
        inputCreator.mapper.Player.Space.performed += _ =>SpaceInputAction?.Invoke();
    }

    protected override void OnStopRunning() {
        inputCreator.mapper.Disable();
    }

    protected override void OnUpdate() {
    }
}*/