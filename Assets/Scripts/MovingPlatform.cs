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
            Debug.Log("Player is on Moving Platform");
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        // Check if the player is on the platform
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the moving Platform");
            other.transform.parent = null;
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
