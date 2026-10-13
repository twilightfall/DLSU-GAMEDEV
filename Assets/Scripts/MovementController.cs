using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.Windows;

public class MovementController : MonoBehaviour
{
    [Header("Setup Variables")]
    [SerializeField]
    CharacterController controller;
    Transform weaponSlot;
    public GameObject currentlyEquippedWeapon;

    // Controller Variables
    bool isControllerConnected = false;
    Gamepad gamepad;

    // Movement Variables
    [SerializeField] float moveSpeed = 10f;
    Vector3 velocity;

    // Jump Variables
    bool jumpPressed;

    // Ground Check
    [Header("Gravity Variables")]
    [SerializeField] Transform groundCheck;
    [SerializeField] bool isGrounded;
    [SerializeField] bool isJumping;
    public LayerMask groundMask;

    public Vector2 moveVector;

    public Transform cam;

    // Look Rotation Variables
    float mouseX, mouseY;
    float xRotation = 0f;

    Vector2 lookVector;

    bool isInteracting;

    [SerializeField] GameObject projectile;
    [SerializeField] Transform source;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        Interact();
        UpdateLook();
        Jump();
        GravityCheck();
        UpdateMove();
    }

    private void Shoot(bool isAttacking)
    {
        if (isAttacking)
        {
            print("bang");
            if (Physics.Raycast(Camera.main.transform.position, Camera.main.transform.forward, out RaycastHit hit, Mathf.Infinity))
            {
                Vector3 direction = hit.point - source.position;
                GameObject p = Instantiate(projectile, source.position, Quaternion.identity);
                p.GetComponent<Rigidbody>().AddForce(direction * 10f, ForceMode.Impulse);
            }
        }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawSphere(groundCheck.position, .15f);
        Gizmos.DrawRay(new(cam.transform.position, cam.transform.forward * 2.5f));
    }

    public void OnMove(InputValue value)
    {
        moveVector = value.Get<Vector2>();
    }

    public void OnJump(InputValue value)
    {
        print("jump pressed");
        jumpPressed = value.isPressed;
    }

    public void OnLook(InputValue value)
    {
        lookVector = value.Get<Vector2>();
    }

    public void OnInteract(InputValue value)
    {

    }

    public void OnAttack(InputValue value)
    {
        Shoot(value.isPressed);
    }

    void UpdateLook()
    {
        if (isControllerConnected == false)
        {
            mouseX = lookVector.x * 50f * Time.deltaTime;
            mouseY = lookVector.y * 50f * Time.deltaTime;

            xRotation -= mouseY;
            xRotation = Mathf.Clamp(xRotation, -89f, 89f);

            transform.Rotate(Vector3.up * mouseX);
            cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }
        else
        {
            Vector2 camRotation = gamepad.rightStick.ReadValue();

            xRotation += camRotation.y;
            xRotation = Mathf.Clamp(xRotation, -89f, 89f);

            transform.Rotate(Vector3.up * camRotation.x);
            cam.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        }

        if (Physics.Raycast(new(cam.transform.position, cam.transform.forward * 2.5f), out RaycastHit hitInfo, 2.5f))
        {
            GameObject target = hitInfo.collider.gameObject;
            if(target.CompareTag("Console"))
            {
                HUDController.instance.SetHUD("Press E to xxxx");
            }
        }
        else
        {
            HUDController.instance.ClearHUD();
        }
    }

    void GravityCheck()
    {
        isGrounded = Physics.CheckSphere(groundCheck.position, .15f, groundMask);
        

        if (isGrounded & velocity.y < 0f)
        {
            jumpPressed = false;
            isJumping = false;
            velocity.y = -1f;
        }
        else
        {
            isJumping = true;
            velocity.y += Physics.gravity.y * Time.deltaTime;
            controller.Move(velocity * Time.deltaTime);
        }
    }

    void Jump()
    {
        if (isControllerConnected == false)
        {
            if (jumpPressed && isGrounded && !isJumping)
            {
                isGrounded = false;
                isJumping = true;

                velocity.y = Mathf.Sqrt(2f * -2f * Physics.gravity.y);
                controller.Move(velocity * Time.deltaTime);
            }
        }
        else
        {
            if (gamepad.aButton.wasPressedThisFrame && isGrounded && !isJumping)
            {
                isGrounded = false;
                isJumping = true;

                velocity.y = Mathf.Sqrt(2f * -2f * Physics.gravity.y);
                controller.Move(velocity * Time.deltaTime);
            }
        }
    }

    void UpdateMove()
    {
        if (isControllerConnected == false)
        {
            Vector3 movement = transform.right * moveVector.x + transform.forward * moveVector.y;

            controller.Move(moveSpeed * Time.deltaTime * movement);
        }
        else
        {
            moveVector = gamepad.leftStick.ReadValue();

            Vector3 movement = transform.right * moveVector.x + transform.forward * moveVector.y;

            controller.Move(5f * Time.deltaTime * movement);
        }
    }

    public void Interact()
    {
        
    }
}
