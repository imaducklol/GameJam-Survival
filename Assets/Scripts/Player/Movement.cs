using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class Movement : MonoBehaviour
{
    private CharacterController characterController;
    private Vector2 movement;
    private Vector2 looking;
    private Camera cam;
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        movement = Vector2.zero;
        cam = GetComponentInChildren<Camera>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(0, looking.x, 0));
        cam.transform.Rotate(looking.y, 0, 0);
    }

    private void FixedUpdate()
    {
        characterController.Move(new Vector3(movement.x, 0, movement.y));
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
        if(context.performed)
        {
            looking = context.ReadValue<Vector2>();
            Debug.Log("test");
        }
        else if(context.canceled)
        {
            looking = Vector2.zero;
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {

    }
}
