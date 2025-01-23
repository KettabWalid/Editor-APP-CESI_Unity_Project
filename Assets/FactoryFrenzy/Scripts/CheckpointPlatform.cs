using UnityEngine;

public class CheckpointPlatform : MonoBehaviour
{
    [SerializeField] private Transform platformStartPosition; // Reference to the platform's start position
    [SerializeField] private GameObject checkpointLight; // Reference to the checkpoint's light (child object)

    private void Start()
    {
        // Ensure the checkpoint light is off at the beginning
        if (checkpointLight != null)
        {
            checkpointLight.SetActive(false);
        }
        else
        {
            Debug.LogError("Checkpoint Light is not assigned! Make sure the light exists as a child object.");
        }
    }

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

    private void OnTriggerEnter(Collider other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            Debug.Log("Checkpoint reached!");

            // Activate the checkpoint light
            if (checkpointLight != null)
            {
                checkpointLight.SetActive(true);
            }
        }
    }
}
