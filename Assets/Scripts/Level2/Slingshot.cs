using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slingshot : MonoBehaviour
{
    [Header("References")]
    public LineRenderer trajectoryLine; // Line Renderer for trajectory visualization
    public GameObject projectilePrefab; // Prefab to spawn and launch
    public Transform spawnPoint; // Where the prefab will spawn

    [Header("Settings")]
    public float launchForceMultiplier = 10f; // Adjust the strength of the launch
    public int trajectorySegments = 20; // Number of segments in the trajectory line

    private Camera mainCamera;
    private Vector3 startDragPosition;
    private Vector3 launchDirection;
    private bool isDragging = false;

    private Rigidbody currentProjectile; // The currently spawned projectile

    private void Start()
    {
        mainCamera = Camera.main;

        // Ensure the trajectory line is disabled at the start
        trajectoryLine.enabled = false;
    }

    private void Update()
    {
        HandleInput();
    }

    private void HandleInput()
    {
        if (Input.GetMouseButtonDown(0)) // Start drag
        {
            StartDrag();
        }
        else if (Input.GetMouseButton(0) && isDragging) // Update drag
        {
            UpdateDrag();
        }
        else if (Input.GetMouseButtonUp(0) && isDragging) // Release drag
        {
            ReleaseDrag();
        }
    }

    private void StartDrag()
    {
        isDragging = true;

        // Spawn a new projectile
        SpawnProjectile();

        // Convert mouse position to world position
        startDragPosition = GetMouseWorldPosition();

        trajectoryLine.enabled = true; // Show trajectory
    }

    private void UpdateDrag()
    {
        Vector3 currentDragPosition = GetMouseWorldPosition();

        // Calculate direction and strength of the drag
        launchDirection = startDragPosition - currentDragPosition;

        // Display trajectory
        VisualizeTrajectory();
    }

    private void ReleaseDrag()
    {
        isDragging = false;
        trajectoryLine.enabled = false; // Hide trajectory

        if (currentProjectile != null)
        {
            // Apply force to the projectile
            currentProjectile.isKinematic = false;
            currentProjectile.AddForce(launchDirection * launchForceMultiplier, ForceMode.Impulse);
        }
    }

    private Vector3 GetMouseWorldPosition()
    {
        // Get mouse position in world space, assuming near the camera
        Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
        if (Physics.Raycast(ray, out RaycastHit hit))
        {
            return hit.point; // Return the point on the collider hit by the ray
        }

        return ray.origin + ray.direction * 10f; // Default fallback
    }

    private void VisualizeTrajectory()
    {
        if (currentProjectile == null)
            return;

        Vector3 startPosition = currentProjectile.transform.position;
        Vector3 velocity = launchDirection * launchForceMultiplier;

        trajectoryLine.positionCount = trajectorySegments;
        for (int i = 0; i < trajectorySegments; i++)
        {
            float t = i * 0.1f; // Time step
            Vector3 point = startPosition + velocity * t + 0.5f * Physics.gravity * t * t; // Equation of motion
            trajectoryLine.SetPosition(i, point);
        }
    }

    private void SpawnProjectile()
    {
        if (projectilePrefab == null || spawnPoint == null)
        {
            Debug.LogError("ProjectilePrefab or SpawnPoint is not assigned!");
            return;
        }

        // Spawn the prefab at the spawn point
        GameObject projectileInstance = Instantiate(
            projectilePrefab,
            spawnPoint.position,
            spawnPoint.rotation
        );

        // Assign its Rigidbody
        currentProjectile = projectileInstance.GetComponent<Rigidbody>();

        // Ensure the Rigidbody is kinematic until released
        currentProjectile.isKinematic = true;
    }
}
