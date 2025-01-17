using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.InputSystem;

public class TPP_Controller : MonoBehaviour
{
    PlayerControls playerControls;
    public CharacterController controller;
    public Transform cam;
    public Animator animator;

    public float speed = 6;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    bool canDoubleJump;
    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    AudioSource audioSource;
    public AudioClip jumpSound;

    private void Awake()
    {
        playerControls = new PlayerControls();
        playerControls.Player.Enable();
        playerControls.Player.Jump.started += jump;
    }

    private void Start()
    {
        audioSource = GetComponent<AudioSource>();
    }

    private void OnDisable()
    {
        playerControls.Player.Disable();
        playerControls.Player.Jump.started -= jump;
    }

    void Update()
    {
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //walk
        Vector2 moveInput = playerControls.Player.Move.ReadValue<Vector2>();
        Vector3 direction = new Vector3(moveInput.x, 0f, moveInput.y).normalized;

        if (direction.magnitude >= 0.1f)
        {
            animator.SetBool("isMoving", true);
            float targetAngle =
                Mathf.Atan2(direction.x, direction.z) * Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(
                transform.eulerAngles.y,
                targetAngle,
                ref turnSmoothVelocity,
                turnSmoothTime
            );
            transform.rotation = Quaternion.Euler(0f, angle, 0f);

            Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            controller.Move(moveDir.normalized * speed * Time.deltaTime);
        }
        else
        {
            animator.SetBool("isMoving", false);
        }
    }

    private void jump(InputAction.CallbackContext context)
    {
        audioSource.clip = jumpSound;

        if (isGrounded)
        {
            audioSource.Play();
            canDoubleJump = true; // Reset double jump when grounded

            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
        }
        else if (canDoubleJump)
        {
            audioSource.Play();
            velocity.y = Mathf.Sqrt(jumpHeight * -2f * gravity);
            canDoubleJump = false; // Disable double jump
        }
    }
}
