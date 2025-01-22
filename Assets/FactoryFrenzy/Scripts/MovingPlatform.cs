using UnityEngine;

public class MovingPlatform : MonoBehaviour
{
    public Transform startPoint; // Reference to StartPoint
    public Transform endPoint;   // Reference to EndPoint
    public float moveDuration = 2f; // Time to move between the points

    private Vector3 initialPosition;
    private Vector3 targetPosition;
    private bool movingToEnd = true;

    private void Start()
    {
        // Store positions but constrain to X-axis
        initialPosition = new Vector3(startPoint.position.x, transform.position.y, transform.position.z);
        targetPosition = new Vector3(endPoint.position.x, transform.position.y, transform.position.z);
    }

    private void Update()
    {
        // Interpolation between the start and end points using Mathf.PingPong
        float t = Mathf.PingPong(Time.time / moveDuration, 1f); // Calculates progress over time
        float xPos = Mathf.Lerp(initialPosition.x, targetPosition.x, t); // Only interpolate X-axis

        // Update platform position (X moves, Y/Z stays the same)
        transform.position = new Vector3(xPos, initialPosition.y, initialPosition.z);
    }
}
