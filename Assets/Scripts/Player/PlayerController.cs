using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float minLookAngleNorm;
    [SerializeField]
    private float maxLookAngleNorm;
    [SerializeField]
    private float projectileVelocity;
    [SerializeField]
    private float movementVelocity;
    [SerializeField]
    private Vector2 lookVelocity;

    private CharacterController characterController;
    private Vector3 movement;
    private Vector2 looking;
    private Camera cam;
    private float angle;
    
    // Start is called before the first frame update
    void Start()
    {
        characterController = GetComponent<CharacterController>();
        movement = Vector2.zero;
        cam = GetComponentInChildren<Camera>();
        angle = cam.transform.localEulerAngles.x;
        Cursor.lockState = CursorLockMode.Locked;
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void FixedUpdate()
    {
        // move the ccharacter
        characterController.Move(transform.TransformDirection(movement));

        // rotate the character
        transform.Rotate(new Vector3(0, looking.x, 0));
        // going past max rotation
        if (NormalizeAngles(angle) + looking.y < minLookAngleNorm)
        {
            looking.y = minLookAngleNorm - NormalizeAngles(cam.transform.localEulerAngles.x);
        }
        else if (NormalizeAngles(angle) + looking.y > maxLookAngleNorm)
        {
            looking.y = maxLookAngleNorm - NormalizeAngles(cam.transform.localEulerAngles.x);
        }
        angle -= looking.y;
        cam.transform.Rotate(-looking.y, 0, 0);
    }

    public void Move(InputAction.CallbackContext context)
    {
        if(context.performed)
        {
            movement = new Vector3(context.ReadValue<Vector2>().x, 0, context.ReadValue<Vector2>().y) * movementVelocity;
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
            looking = context.ReadValue<Vector2>() * lookVelocity;
        }
        else if(context.canceled)
        {
            looking = Vector2.zero;
        }
    }

    public void Fire(InputAction.CallbackContext context)
    {
        if(context.started)
        {
            GameObject projectile = GameObject.CreatePrimitive(PrimitiveType.Sphere);
            projectile.AddComponent<Rigidbody>();
            projectile.AddComponent<Ballz>();
            projectile.transform.position = this.GetComponentInChildren<Camera>().transform.position;
            projectile.GetComponent<Rigidbody>().AddForce(this.GetComponentInChildren<Camera>().transform.forward * projectileVelocity);
        }
    }

    public float NormalizeAngles(float input)
    {
        return -(input - 180f - Mathf.Sign(input - 180f) * 180f);
    }
}
