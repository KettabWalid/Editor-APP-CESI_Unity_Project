using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public float offsetDistance = 1f;  // Distance the platform moves in positive and negative X directions
    public float moveDuration = 2f;   // Time to complete one movement cycle

    private Vector3 startPosition;    // Starting position of the platform
    private float minX;               // Minimum X position
    private float maxX;               // Maximum X position

    private void Start()
    {
        // Store the platform's initial position
        startPosition = transform.position;

        // Calculate the range of movement on the X-axis
        minX = startPosition.x - offsetDistance;
        maxX = startPosition.x + offsetDistance;
    }

    private void Update()
    {
        // Interpolate between the min and max X positions using Mathf.PingPong
        float t = Mathf.PingPong(Time.time / moveDuration, 1f);
        float xPos = Mathf.Lerp(minX, maxX, t);

        // Update the platform's position (move only on X-axis)
        transform.position = new Vector3(xPos, startPosition.y, startPosition.z);
    }
}
