using UnityEngine;

public class CheckpointPlatform : MonoBehaviour
{
    [SerializeField] private Transform platformStartPosition; // Reference to the platform's start position

    // Getter for respawn position
    public Vector3 GetRespawnPosition()
    {
        if (platformStartPosition != null)
        {
            return platformStartPosition.position; // Return platform's start position
        }
        else
        {
            Debug.LogError("Platform Start Position is not set!");
            return transform.position; // Fallback: use checkpoint's position
        }
    }
}
