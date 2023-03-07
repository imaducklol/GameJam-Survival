using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float minLookAngleNorm;
    [SerializeField]
    private float maxLookAngleNorm;
    [SerializeField]
    private float projectileVelocity;
    [SerializeField] 
    private float projectilesPerSecond;
    [SerializeField] 
    private float regenPerSecond;
    [SerializeField]
    private float movementVelocity;
    [SerializeField]
    private Vector2 lookVelocity;
    [SerializeField]
    private int startingHealth;

    private CharacterController characterController;
    private Vector3 movement;
    private Vector2 looking;
    private Camera cam;
    private float angle;
    public int health;
    private float regenTimer = 0;
    private float projectileTimer = 0;

    [SerializeField] 
    private GameObject bulletPrefab;
    public GameObject healthDisplay;
    public GameObject deathDisplay;
    public GameObject restartDisplay;
    public TMPro.TMP_Text healthString;
    

    // Start is called before the first frame update
    void Start()
    {
        health = startingHealth;
        characterController = GetComponent<CharacterController>();
        movement = Vector2.zero;
        cam = GetComponentInChildren<Camera>();
        angle = cam.transform.localEulerAngles.x;
        Cursor.lockState = CursorLockMode.Locked;
        
        healthString = healthDisplay.GetComponent<TMPro.TMP_Text>();
        healthString.text = "Health: " + health;
    }

    // Update is called once per frame
    void Update()
    {
        projectileTimer += Time.deltaTime;
        if (health < startingHealth && health > 0) regenTimer += Time.deltaTime;

        if (regenTimer > 1 / regenPerSecond && health < startingHealth)
        {
            health += 1;
            healthString.text = "Health: " + health;
            regenTimer = 0;
        }
        
        if(health < 1)
        {
            Kill();
        }
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
        if(context.started && projectileTimer > 1/projectilesPerSecond)
        {
            projectileTimer = 0;
            Vector3 performanceCam = cam.transform.forward;
            GameObject projectile = Instantiate(bulletPrefab);
            projectile.transform.position = cam.transform.position + performanceCam;
            projectile.GetComponent<Rigidbody>().AddForce(performanceCam * projectileVelocity);
        }
    }

    public float NormalizeAngles(float input)
    {
        return -(input - 180f - Mathf.Sign(input - 180f) * 180f);
    }

    public void Kill()
    {
        deathDisplay.SetActive(true);
        movementVelocity = .001f;
        projectileTimer = 0;
        lookVelocity = new Vector2(lookVelocity.x * .5f, lookVelocity.y *.5f);
        Debug.Log("Died");
        StartCoroutine(StartRestartTimer());

    }
    IEnumerator StartRestartTimer()
    {
        //yield on a new YieldInstruction that waits for 5 seconds.
        yield return new WaitForSeconds(1.5f);

        //After we have waited 5 seconds print the time again.
        restartDisplay.gameObject.SetActive(true);
        Debug.Log("Set Restart object to active");
        Cursor.lockState = CursorLockMode.None;
    }
}
