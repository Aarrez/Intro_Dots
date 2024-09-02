using UnityEngine;
using UnityEngine.InputSystem;

public class MainShip : MonoBehaviour
{
    [SerializeField] private float moveSpeed = 5f;
    [SerializeField] private float rotationSpeed = 500f;
    
    private DetachedInputSender inputSender;
    private Vector3 moveVector;
   
    private bool isMoveing;

    private void OnEnable()
    {
        DetachedInputSender.WasdInputAction += WasdInput;
    }
    private void OnDisable()
    {
        DetachedInputSender.WasdInputAction -= WasdInput;
    }

    private void WasdInput(InputAction.CallbackContext arg)
    {
        isMoveing = arg.performed;
        moveVector = new Vector3(arg.ReadValue<Vector2>().x, arg.ReadValue<Vector2>().y, 0);
        
    }

    private void MoveShip()
    {
        transform.position += moveVector * (moveSpeed * Time.deltaTime);
    }

    private void RotateShip()
    {
        if(!isMoveing) return;
        Quaternion toRotation = Quaternion.LookRotation(Vector3.forward,moveVector); 
        transform.rotation = Quaternion.RotateTowards(
            transform.rotation, 
            toRotation, 
            rotationSpeed * Time.deltaTime);
    }

    private void Update()
    {
        MoveShip();
        RotateShip();
    }
}