using UnityEngine;
using System.Collections.Generic;

public class TrapShooter : MonoBehaviour
{
    public enum State { Search, Orientation, Shooting }

    [Header("State Configuration")]
    public State currentState = State.Search;

    [Header("Search Configuration")]
    public float rotationSpeed = 20f;
    public float leftAngle = -45f;
    public float rightAngle = 45f;

    [Header("Orientation Configuration")]
    public float turnSpeed = 5f;
    public float orientationThreshold = 30f;

    [Header("Shooting Configuration")]
    public GameObject bulletPrefab;
    public Transform firePoint;
    public float fireRate = 1f;

    [Header("Detection Configuration")]
    public LayerMask detectionMask;
    public float detectionRadius = 10f;

    [Header("Knockback Configuration")]
    public float knockbackForce = 10f;

    private Transform targetPlayer;
    private float lastFireTime = 0f;
    private float currentAngle = 0f;
    private bool rotatingRight = true;
    private List<Transform> detectedPlayers = new List<Transform>();

    private void Update()
    {
        switch (currentState)
        {
            case State.Search:
                SearchState();
                break;

            case State.Orientation:
                OrientationState();
                break;

            case State.Shooting:
                ShootingState();
                break;
        }
    }

    private void SearchState()
    {
        currentAngle = transform.eulerAngles.y;

        if (rotatingRight && currentAngle >= rightAngle)
        {
            rotatingRight = false;
        }
        else if (!rotatingRight && currentAngle <= leftAngle)
        {
            rotatingRight = true;
        }

        float rotationDirection = rotatingRight ? 1 : -1;
        transform.Rotate(Vector3.up, rotationDirection * rotationSpeed * Time.deltaTime);

        detectedPlayers = DetectPlayersInRange();

        if (detectedPlayers.Count > 0)
        {
            targetPlayer = detectedPlayers[0]; // Select the first detected player
            currentState = State.Orientation;
        }
    }

    private void OrientationState()
    {
        if (targetPlayer == null)
        {
            currentState = State.Search;
            return;
        }

        // Track the player continuously by calculating the direction towards the player
        Vector3 directionToPlayer = (targetPlayer.position - transform.position).normalized;

        // Update rotation to face the player
        Quaternion lookRotation = Quaternion.LookRotation(directionToPlayer);
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * turnSpeed);

        float angleToPlayer = Vector3.Angle(transform.forward, directionToPlayer);

        // If within the shooting range, go to shooting state
        if (angleToPlayer <= orientationThreshold)
        {
            currentState = State.Shooting;
        }

        // Remove player if out of detection range
        if (Vector3.Distance(transform.position, targetPlayer.position) > detectionRadius)
        {
            targetPlayer = null;
            currentState = State.Search;
        }
    }

    private void ShootingState()
    {
        if (targetPlayer == null)
        {
            currentState = State.Search;
            return;
        }

        // Fire bullet if enough time has passed
        if (Time.time - lastFireTime >= 1 / fireRate)
        {
            lastFireTime = Time.time;
            FireBullet();
        }

        // Remove player if out of detection range
        if (Vector3.Distance(transform.position, targetPlayer.position) > detectionRadius)
        {
            targetPlayer = null;
            currentState = State.Search;
        }
    }

    private void FireBullet()
    {
        if (firePoint == null || bulletPrefab == null)
        {
            return;
        }

        // Get the direction from the shooter to the target player
        Vector3 directionToPlayer = (targetPlayer.position - firePoint.position).normalized;

        // Fire a bullet towards the target player
        GameObject bullet = Instantiate(bulletPrefab, firePoint.position, Quaternion.LookRotation(directionToPlayer));
        Bullet bulletScript = bullet.GetComponent<Bullet>();

        if (bulletScript != null)
        {
            bulletScript.SetKnockbackForce(knockbackForce);
        }
    }

    private List<Transform> DetectPlayersInRange()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, detectionMask);
        List<Transform> players = new List<Transform>();

        foreach (Collider col in colliders)
        {
            if (col.CompareTag("Player"))
            {
                players.Add(col.transform);
            }
        }
        return players;
    }
}
