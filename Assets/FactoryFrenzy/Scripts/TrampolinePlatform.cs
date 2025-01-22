using UnityEngine;

public class TrampolinePlatform : MonoBehaviour
{
    [SerializeField] private float bounceForce = 10f; // Force of the trampoline

    private void OnCollisionEnter(Collision collision)
    {
        // Check if the collided object has a Rigidbody
        Rigidbody rb = collision.gameObject.GetComponent<Rigidbody>();
        if (rb != null)
        {
            // Apply upward force to the object
            Vector3 bounce = Vector3.up * bounceForce;
            rb.AddForce(bounce, ForceMode.Impulse);
        }
    }
}
