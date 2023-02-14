using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector2 movement;
    private Vector2 looking;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        movement = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void FixedUpdate()
    {
        characterController.Move(new Vector3(movement.x, 0, movement.y));
        Debug.Log(movement);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            movement = context.ReadValue<Vector2>();
        }
        else if(context.canceled)
        {
            movement = Vector2.zero;
        }
    }

    public void Look(InputAction.CallbackContext context)
    {
        looking += context.ReadValue<Vector2>();
    }

    public void Fire(InputAction.CallbackContext context)
    {

    }
}
