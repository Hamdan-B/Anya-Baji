using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float targetX = 10f; // Target x-axis position relative to the initial position

    [SerializeField]
    private float speed = 2f; // Speed of the platform

    private Vector3 startPosition; // Initial position of the platform
    private Vector3 targetPosition; // Target position of the platform
    private bool movingToTarget = true; // Direction of movement

    private CharacterController cc; // Reference to the character controller on the player
    private Vector3 previousPosition; // To track platform's previous position

    void Start()
    {
        startPosition = transform.localPosition;
        targetPosition = new Vector3(startPosition.x + targetX, startPosition.y, startPosition.z);
    }

    void Update()
    {
        MovePlatform();
    }

    private void MovePlatform()
    {
        // Determine the destination
        Vector3 destination = movingToTarget ? targetPosition : startPosition;

        // Move the platform
        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition,
            destination,
            speed * Time.deltaTime
        );

        // Check if the platform has reached the destination
        if (Vector3.Distance(transform.localPosition, destination) < 0.01f)
        {
            movingToTarget = !movingToTarget; // Switch direction
        }
    }

    void OnTriggerEnter(Collider other)
    {
        // Check if the player is on the platform
        if (other.CompareTag("Player"))
        {
            cc = other.GetComponent<CharacterController>();
            previousPosition = transform.position; // Save the platform's position when player enters
        }
    }

    void OnTriggerStay(Collider other)
    {
        // Move the player with the platform
        if (other.CompareTag("Player") && cc != null)
        {
            // Move the player relative to the platform's movement
            cc.Move(transform.position - previousPosition);
            previousPosition = transform.position; // Update the previous position of the platform
        }
    }

    void OnDrawGizmos()
    {
        // Draw the movement path for visualization in the editor
        if (Application.isPlaying)
            return;
        Vector3 initialPosition = transform.localPosition;
        Vector3 endPosition = new Vector3(
            initialPosition.x + targetX,
            initialPosition.y,
            initialPosition.z
        );

        Gizmos.color = Color.green;
        Gizmos.DrawLine(initialPosition, endPosition);
        Gizmos.DrawSphere(endPosition, 0.2f);
    }
}
