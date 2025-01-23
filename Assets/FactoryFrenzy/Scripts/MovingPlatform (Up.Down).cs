using UnityEngine;

public class MovingPlatform1 : MonoBehaviour
{
    public float offsetDistance = 1f;  // Distance the platform moves in positive and negative Y directions
    public float moveDuration = 2f;   // Time to complete one movement cycle

    private Vector3 startPosition;    // Starting position of the platform
    private float minY;               // Minimum Y position
    private float maxY;               // Maximum Y position

    private void Start()
    {
        // Store the platform's initial position
        startPosition = transform.position;

        // Calculate the range of movement on the Y-axis
        minY = startPosition.y - offsetDistance;
        maxY = startPosition.y + offsetDistance;
    }

    private void Update()
    {
        // Interpolate between the min and max Y positions using Mathf.PingPong
        float t = Mathf.PingPong(Time.time / moveDuration, 1f);
        float yPos = Mathf.Lerp(minY, maxY, t);

        // Update the platform's position (move only on Y-axis)
        transform.position = new Vector3(startPosition.x, yPos, startPosition.z);
    }
}
