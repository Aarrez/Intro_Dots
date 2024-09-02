using UnityEngine;
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


public class DetachedInputSender
{
    private static InputCreator inputCreator;
    public static UnityAction<InputAction.CallbackContext> WasdInputAction;
    public static UnityAction SpaceInputAction;

    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.BeforeSceneLoad)]
    private static void InitializeInput()
    {
        inputCreator = new InputCreator();
        inputCreator.mapper.Enable();
    }
    
    [RuntimeInitializeOnLoadMethod(RuntimeInitializeLoadType.AfterSceneLoad)]
    private static void SendInput()
    {
        inputCreator.mapper.Player.WASD.performed += ctx =>WasdInputAction?.Invoke(ctx);
        inputCreator.mapper.Player.WASD.canceled += arg0 => WasdInputAction?.Invoke(arg0);
        
        inputCreator.mapper.Player.Space.performed += _ =>SpaceInputAction?.Invoke();
    }
}