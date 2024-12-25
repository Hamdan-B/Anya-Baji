using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CarController2 : MonoBehaviour
{
    [Header("Car Settings")]
    [SerializeField]
    private float speed = 10f;

    [SerializeField]
    private float turnSpeed = 50f;

    [Header("Gravity Settings")]
    [SerializeField]
    private float gravity = 9.8f;

    [SerializeField]
    private LayerMask groundLayer;

    [SerializeField]
    private float groundCheckDistance = 0.5f;
    private Rigidbody rb;
    private float horizontalInput;
    private float verticalInput;
    private bool isGrounded;

    private void Start()
    {
        rb = GetComponent<Rigidbody>();
        if (rb == null)
        {
            rb = gameObject.AddComponent<Rigidbody>();
        }

        rb.useGravity = false;
        rb.constraints =
            RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void Update()
    {
        GetInput();
    }

    private void FixedUpdate()
    {
        Move();
        Turn();
        ApplyGravity();
    }

    private void GetInput()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
    }

    private void Move()
    {
        Vector3 forwardMovement = transform.forward * verticalInput * speed;
        rb.MovePosition(rb.position + forwardMovement * Time.fixedDeltaTime);
    }

    private void Turn()
    {
        float turn = horizontalInput * turnSpeed * Time.fixedDeltaTime;
        Quaternion turnRotation = Quaternion.Euler(0f, turn, 0f);
        rb.MoveRotation(rb.rotation * turnRotation);
    }

    private void ApplyGravity()
    {
        isGrounded = Physics.Raycast(
            transform.position,
            Vector3.down,
            groundCheckDistance,
            groundLayer
        );

        if (!isGrounded)
        {
            rb.MovePosition(rb.position + Vector3.down * gravity * Time.fixedDeltaTime);
        }
    }
}
