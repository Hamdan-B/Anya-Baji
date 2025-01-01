using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    [SerializeField]
    private float targetX = 10f;

    [SerializeField]
    private float speed = 2f;

    private Vector3 startPosition;
    private Vector3 targetPosition;
    private bool movingToTarget = true;

    private CharacterController cc;
    private Vector3 previousPosition;

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
        Vector3 destination = movingToTarget ? targetPosition : startPosition;

        transform.localPosition = Vector3.MoveTowards(
            transform.localPosition,
            destination,
            speed * Time.deltaTime
        );

        if (Vector3.Distance(transform.localPosition, destination) < 0.01f)
        {
            movingToTarget = !movingToTarget;
        }
    }

    void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player is on Moving Platform");
            other.transform.parent = transform;
        }
    }

    void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            Debug.Log("Player left the moving Platform");
            other.transform.parent = null;
        }
    }

    void OnDrawGizmos()
    {
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
