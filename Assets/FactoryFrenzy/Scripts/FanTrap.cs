using UnityEngine;

public class FanTrap : MonoBehaviour
{
    [SerializeField] private Transform wheel; // Reference to the rotating wheel object
    [SerializeField] private float spinSpeed = 500f; // Speed of the wheel spinning
    [SerializeField] private float pushForce = 50f; // Force applied to the player

    private void Update()
    {
        // Continuously spin the WHEEL
        if (wheel != null)
        {
            // Adjust rotation axis if necessary (e.g., Vector3.right or Vector3.forward)
            wheel.Rotate(Vector3.back * spinSpeed * Time.deltaTime);
        }
    }

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Push the player backward
            Rigidbody playerRigidbody = other.GetComponent<Rigidbody>();
            if (playerRigidbody != null)
            {
                Vector3 forceDirection = (other.transform.position - transform.position).normalized; // Push away from the fan
                forceDirection.y = 0; // Ignore vertical forces
                playerRigidbody.AddForce(forceDirection * pushForce, ForceMode.Force); // Apply force
            }
        }
    }
}
