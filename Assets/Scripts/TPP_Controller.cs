using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEngine;

public class TPP_Controller : MonoBehaviour
{
    // public CharacterController cc;
    // public Transform mainCam;

    // public float moveSpeed;
    // public float rotSmooth;
    // public float jumpHeight = 1.0f;
    // public bool isGrounded = true;


    // private Vector2 inputVal;
    // private Vector3 dir;
    // private float rotSmoothVel;
    // private Vector3 playerVelocity;

    // private float gravityValue = -9.81f;

    // private void Start() {
    //     Cursor.lockState = CursorLockMode.Locked;
    // }

    // void Update()
    // {
    //     isGrounded = cc.isGrounded;
    //     inputVal = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
    //     dir = new Vector3(inputVal.x, 0f, inputVal.y);

    //     if(dir.magnitude >= 0.1f){
    //         float targetAngle = Mathf.Atan2(dir.x, dir.z) * Mathf.Rad2Deg + mainCam.eulerAngles.y;
    //         float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref rotSmoothVel, rotSmooth);
    //         transform.rotation = Quaternion.Euler(transform.eulerAngles.x, angle, transform.eulerAngles.z);

    //         Vector3 moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;

    //         cc.Move(moveDir.normalized * moveSpeed * Time.deltaTime);
    //     }

    // }




    public CharacterController controller;
    public Transform cam;
    public Animator animator;

    public float speed = 6;
    public float gravity = -9.81f;
    public float jumpHeight = 3;
    Vector3 velocity;
    bool isGrounded;

    public Transform groundCheck;
    public float groundDistance = 0.4f;
    public LayerMask groundMask;

    float turnSmoothVelocity;
    public float turnSmoothTime = 0.1f;

    // Update is called once per frame
    void Update()
    {
        //jump
        isGrounded = Physics.CheckSphere(groundCheck.position, groundDistance, groundMask);

        if (isGrounded && velocity.y < 0)
        {
            velocity.y = -2f;
        }

        if (Input.GetButtonDown("Jump") && isGrounded)
        {
            velocity.y = Mathf.Sqrt(jumpHeight * -2 * gravity);
        }
        //gravity
        velocity.y += gravity * Time.deltaTime;
        controller.Move(velocity * Time.deltaTime);
        //walk
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

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
}
