using Unity.Mathematics;
using UnityEngine;

public class PlayerRespawn : MonoBehaviour
{
    [SerializeField] private Transform startGamePosition; // Game start position
    private Vector3 respawnPosition;
    [SerializeField] private Transform respawnTransform;
    private void Start()
    {
        // Initialize respawn to game start position
        if (startGamePosition != null)
        {
            respawnPosition = startGamePosition.position;
        }
        else
        {
            Debug.LogError("Start Game Position is not set!");
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Checkpoint"))
        {
            // Update the respawn position to the platform's start position
            CheckpointPlatform checkpoint = other.GetComponent<CheckpointPlatform>();
            if (checkpoint != null)
            {
                respawnPosition = checkpoint.GetRespawnPosition();
                Debug.Log("Checkpoint reached! Respawn position updated to platform start.");
            }
        }
        else if (other.CompareTag("FallZone"))
        {
            // Respawn at the current respawn position
            transform.position = respawnPosition;
            //  gameObject.transform =   respawnTransform;
            transform.rotation = quaternion.identity;
            Debug.Log("Player fell and respawned.");
        }
    }
}
