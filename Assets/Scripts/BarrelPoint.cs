using System;
using Cysharp.Threading.Tasks;
using UnityEngine;
using UnityEngine.InputSystem;

public class BarrelPoint : MonoBehaviour
{
    
    
    private Vector2 MoveDir;
    
    private void OnEnable()
    {
        DetachedInputSender.SpaceInputAction += Shoot;
    }

    private void GetMoveDirection(InputAction.CallbackContext context)
    {
        MoveDir = context.ReadValue<Vector2>();
    }

    private void Shoot()
    {
        
    }
}